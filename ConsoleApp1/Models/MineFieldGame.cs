namespace Game.Models
{
    public class MineFieldGame(Player player, MineField[,] mineFields)
    {
        public const string Alphabet = "ABCDEFGH";
        private const int MineProbability = 30;

        private ConsoleKey[] _allowedKeys =
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow,
        };

        public Player Player { get; init; } = player;
        public static readonly int FieldsPerDirection = 8;
        public MineField[,] MineFields { get; init; } = mineFields;
        public bool IsQuestCompleted => Player.CurrentPosition.Y == (FieldsPerDirection - 1);
        public bool IsPlayerAlive => Player.RemainingLives > 0;
        public void Start()
        {
            do
            {
                WriteInGameStatistics();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (!_allowedKeys.Any(key => key == keyInfo.Key))
                {
                    Console.WriteLine("Error: Invalid key. Please press an arrow key.");
                    continue;
                }

                Player.Play(this, keyInfo.Key);
            }
            while (IsPlayerAlive && !IsQuestCompleted);

            if (!IsPlayerAlive)
            {
                Console.WriteLine("You have no lifes left...");
                return;
            }

            Console.WriteLine("Success. You completed the quest...");
            Console.WriteLine($"Lifes: {Player.RemainingLives}");
            Console.WriteLine($"Total moves: {Player.TotalMoves}");
        }


        public bool IsMoveValid(MineField nextPosition)
        {
            return nextPosition.X >= 0 && nextPosition.X < FieldsPerDirection &&
              nextPosition.Y >= 0 && nextPosition.Y < FieldsPerDirection;
        }

        public bool ProcessMove(MineField nextPosition)
        {
            var mineField = MineFields[nextPosition.X, nextPosition.Y];
            if (Player.CurrentPosition.X == nextPosition.X &&
                Player.CurrentPosition.Y == nextPosition.Y)
            {
                return false;
            }

            if (mineField.HasMineExploded)
            {
                Console.WriteLine("Field already exploded");
                return false;
            }

            if (mineField.HasMine)
            {
                Player.LoseLife();
                mineField.HasMineExploded = true;
                Console.WriteLine("OOOOPS.... You hit the mine.");
            }

            return true;
        }

        public static MineField[,] GenerateMineField(int probability = MineProbability)
        {
            var random = new Random();
            var mineField = new MineField[FieldsPerDirection, FieldsPerDirection];

            for (var i = 0; i < FieldsPerDirection; i++)
            {
                for (var j = 0; j < FieldsPerDirection; j++)
                {
                    mineField[i, j] = new MineField(i, j, random.Next(0, 100) < probability);
                }
            }

            return mineField;
        }

        private void WriteInGameStatistics()
        {
            var stats = GetInGameStatistics();
            Console.WriteLine(stats);
        }

        public string GetInGameStatistics()
        {
            return "---------------------------------------------\n" +
                   $"Current player position: {Alphabet[Player.CurrentPosition.X]}-{Player.CurrentPosition.Y + 1}\n" +
                   $"Lifes left: {Player.RemainingLives}\n" +
                   $"Total moves: {Player.TotalMoves}\n" +
                   "---------------------------------------------\n";
        }
    }
}
