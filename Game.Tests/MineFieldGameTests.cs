using Game.Interfaces;
using Game.Models;

namespace Game.Tests
{
    public class MineFieldGameTests
    {
        private IPlayer _player;
        private IGame _game;

        private static MineField[,] CreateTestMineField(bool shouldMarkAsAlreadyExploded, params (int x, int y)[] mines)
        {
            var mineField = new MineField[MineFieldGame.FieldsPerDirection, MineFieldGame.FieldsPerDirection];
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    var hasMine = mines.Contains((i, j));
                    mineField[i, j] = new MineField(i, j, hasMine)
                    {
                        HasMineExploded = shouldMarkAsAlreadyExploded
                    };
                }
            }
            return mineField;
        }

        [SetUp]
        public void Setup()
        {
            var mineFields = MineFieldGame.GenerateMineField(0);
            var startingPosition = mineFields[0, 0];
            _player = new Player(startingPosition);
            _game = new MineFieldGame(_player, mineFields);
        }

        [Test]
        public void Player_HitsMine_LosesLife()
        {
            _game = new MineFieldGame(_player, CreateTestMineField(false, (1, 1)));
            _player.Play(_game, ConsoleKey.RightArrow);
            _player.Play(_game, ConsoleKey.UpArrow);

            Assert.That(_player.RemainingLives, Is.EqualTo(2));
        }

        [Test]
        public void Player_UpdatesPosition_AfterValidMove()
        {
            _player.Play(_game, ConsoleKey.RightArrow); // Move to (1, 0)

            Assert.Multiple(() =>
            {
                Assert.That(_player.CurrentPosition.X, Is.EqualTo(1));
                Assert.That(_player.CurrentPosition.Y, Is.EqualTo(0));
                Assert.That(_player.TotalMoves, Is.EqualTo(1));
            });
        }

        [Test]
        public void Player_CannotMove_OutsideBoundaries()
        {
            _player.Play(_game, ConsoleKey.LeftArrow);

            Assert.Multiple(() =>
            {
                Assert.That(_player.CurrentPosition.X, Is.EqualTo(0));
                Assert.That(_player.CurrentPosition.Y, Is.EqualTo(0));
            });
        }

        [Test]
        public void Player_CannotMoveUp_FieldAlreadyExploded()
        {
            _game = new MineFieldGame(_player, CreateTestMineField(true, (1, 1)));
            _player.Play(_game, ConsoleKey.RightArrow);
            _player.Play(_game, ConsoleKey.UpArrow);

            Assert.Multiple(() =>
            {
                Assert.That(_player.CurrentPosition.X, Is.EqualTo(0));
                Assert.That(_player.CurrentPosition.Y, Is.EqualTo(0));
                Assert.That(_player.RemainingLives, Is.EqualTo(3));
            });
        }

        [Test]
        public void Player_MoveUpSevenTimes_GameCompleted()
        {
            for (int i = 0; i < 7; i++)
            {
                _player.Play(_game, ConsoleKey.UpArrow);
            }

            Assert.Multiple(() =>
            {
                Assert.That(_player.CurrentPosition.X, Is.EqualTo(0));
                Assert.That(_player.CurrentPosition.Y, Is.EqualTo(7));
                Assert.That(_player.RemainingLives, Is.EqualTo(3));
                Assert.That(_player.TotalMoves, Is.EqualTo(7));
                Assert.That(_game.IsQuestCompleted, Is.True);
            });
        }

        [Test]
        public void Player_CannotMove_InvalidCommand()
        {
            _player.Play(_game, ConsoleKey.N);

            Assert.Multiple(() =>
            {
                Assert.That(_player.CurrentPosition.X, Is.EqualTo(0));
                Assert.That(_player.CurrentPosition.Y, Is.EqualTo(0));
                Assert.That(_player.RemainingLives, Is.EqualTo(3));
                Assert.That(_player.TotalMoves, Is.EqualTo(0));
            });
        }

        [Test]
        public void Player_LosesAllLives_LosesTheGame()
        {
            var mineFields = CreateTestMineField(false, (0, 1), (0, 2), (0, 3));
            _game = new MineFieldGame(_player, mineFields);

            for (int i = 0; i < 3; i++)
            {
                _player.Play(_game, ConsoleKey.UpArrow);
            }

            Assert.Multiple(() =>
            {
                Assert.That(_player.CurrentPosition.X, Is.EqualTo(0));
                Assert.That(_player.CurrentPosition.Y, Is.EqualTo(3));
                Assert.That(_player.RemainingLives, Is.EqualTo(0));
                Assert.That(_player.TotalMoves, Is.EqualTo(3));
                Assert.That(_game.IsQuestCompleted, Is.False);
                Assert.That(_game.IsPlayerAlive, Is.False);
            });
        }
    }
}
