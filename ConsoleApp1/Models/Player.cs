namespace Game.Models
{
    public class Player(MineField startingPosition)
    {
        private const int TotalLives = 3;

        public int RemainingLives { get; private set; } = TotalLives;
        public int TotalMoves { get; private set; }
        public MineField CurrentPosition { get; private set; } = startingPosition;

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

        private MineField GetNextPosition(ConsoleKey direction)
        {
            var nextPosition = new MineField(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.HasMine);

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
