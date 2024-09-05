namespace Game.Models
{
    public class Player()
    {
        private const int TotalLives = 3;

        public int RemainingLives { get; private set; } = TotalLives;
        public int TotalMoves { get; private set; }
        public Position CurrentPosition { get; private set; } = new(0, 0);

        public void Play(MineFieldGame game, ConsoleKey direction)
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

        private Position GetNextPosition(ConsoleKey direction)
        {
            var nextPosition = new Position(CurrentPosition.X, CurrentPosition.Y);

            switch (direction)
            {
                case ConsoleKey.UpArrow:
                    nextPosition.Y += 1;
                    break;
                case ConsoleKey.DownArrow:
                    nextPosition.Y -= 1;
                    break;
                case ConsoleKey.LeftArrow:
                    nextPosition.X -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    nextPosition.X += 1;
                    break;
            }

            return nextPosition;
        }
    }
}
