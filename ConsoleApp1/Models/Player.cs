using Game.Interfaces;

namespace Game.Models
{
    public class Player : IPlayer
    {
        private const int TotalLives = 3;

        public int RemainingLives { get; private set; } = TotalLives;
        public int TotalMoves { get; private set; }
        public MineField CurrentPosition { get; private set; }

        public Player(MineField startingPosition)
        {
            CurrentPosition = startingPosition;
        }

        public void Play(IGame game, ConsoleKey direction)
        {
            var nextPosition = GetNextPosition(direction);

            if (!game.IsMoveValid(nextPosition))
            {
                Console.WriteLine("Unable to move in that direction");
                return;
            }

            if (game.ProcessMove(nextPosition))
            {
                CurrentPosition = nextPosition;
                TotalMoves++;
            }
        }

        public void LoseLife()
        {
            RemainingLives--;
        }

        private MineField GetNextPosition(ConsoleKey direction)
        {
            var nextPosition = new MineField(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.HasMine);

            switch (direction)
            {
                case ConsoleKey.UpArrow:
                    nextPosition.Y++;
                    break;
                case ConsoleKey.DownArrow:
                    nextPosition.Y--;
                    break;
                case ConsoleKey.LeftArrow:
                    nextPosition.X--;
                    break;
                case ConsoleKey.RightArrow:
                    nextPosition.X++;
                    break;
            }

            return nextPosition;
        }
    }
}