namespace ConsoleApp1.Models
{
    public class Player(int startPositionX, int startPositionXY)
    {
        private const int TotalLives = 3;

        public int RemainingLives { get; private set; } = TotalLives;
        public int TotalMoves { get; private set; }
        public Position CurrentPosition { get; private set; } = new(startPositionX, startPositionXY);

        public void Play(Game game, ConsoleKey direction)
        {
            var nextPosition = Move(game, direction);
            if (nextPosition == CurrentPosition)
            {
                Console.WriteLine("Unable to move in that direction");
                return;
            }

            CurrentPosition = nextPosition;
            TotalMoves++;

            CheckForMineFields(game);
        }

        private void CheckForMineFields(Game game)
        {
            var isMineField = game.Mines[CurrentPosition.X, CurrentPosition.Y];
            if (isMineField)
            {
                RemainingLives--;
                Console.WriteLine("OOOOPS.... You hit the mine.");
            }
        }

        private bool IsMoveValid(Position position, Game game, ConsoleKey direction)
        {
            switch (direction)
            {
                case ConsoleKey.UpArrow:
                    return position.Y > 0;
                case ConsoleKey.DownArrow:
                    return position.Y < game.FieldsPerDirection - 1;
                case ConsoleKey.LeftArrow:
                    return position.X > 0;
                case ConsoleKey.RightArrow:
                    return position.X < game.FieldsPerDirection - 1;
                default:
                    return false;
            }
        }

        private Position Move(Game game, ConsoleKey direction)
        {
            var nextPosition = new Position(CurrentPosition.X, CurrentPosition.Y);

            if (IsMoveValid(nextPosition, game, direction))
            {
                switch (direction)
                {
                    case ConsoleKey.UpArrow:
                        nextPosition.Y -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        nextPosition.Y += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        nextPosition.X -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        nextPosition.X += 1;
                        break;
                }
            }

            return nextPosition;
        }
    }
}
