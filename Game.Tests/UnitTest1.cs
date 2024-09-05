using Game.Models;

namespace Game.Tests
{
    public class MineFieldGameTests
    {
        Player _player;
        MineFieldGame _game;
        private MineField[,] CreateTestMineField(bool shouldMarkAsAlreadyExploded)
        {
            var mineField = new MineField[MineFieldGame.FieldsPerDirection, MineFieldGame.FieldsPerDirection];

            // Set a mine at (1, 1)
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    mineField[i, j] = new MineField(i, j, (i == 1 && j == 1))
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
            var mineField = CreateTestMineField(false);
            _game = new MineFieldGame(_player, mineField);

            _game.Player.Play(_game, ConsoleKey.RightArrow);
            _game.Player.Play(_game, ConsoleKey.UpArrow);

            Assert.That(_game.Player.RemainingLives, Is.EqualTo(2));
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
            var mineFields = CreateTestMineField(true);
            _game = new MineFieldGame(_player, mineFields);

            _game.Player.Play(_game, ConsoleKey.RightArrow);
            _game.Player.Play(_game, ConsoleKey.UpArrow);

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
                Assert.That(_game.IsQuestCompleted, Is.EqualTo(true));
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
        public void Player_LoseAllLifes_LoseTheGame()
        {
            //Crate minefield with 3 mines (0, 1) (0, 2) (0, 3)
            var mineField = new MineField[MineFieldGame.FieldsPerDirection, MineFieldGame.FieldsPerDirection];
            _game = new MineFieldGame(_player, mineField);

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    mineField[i, j] = new MineField(i, j, (i == 0 && j <= 3));
                }
            }

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
                Assert.That(_game.IsQuestCompleted, Is.EqualTo(false));
                Assert.That(_game.IsPlayerAlive, Is.EqualTo(false));
            });
        }
    }
}