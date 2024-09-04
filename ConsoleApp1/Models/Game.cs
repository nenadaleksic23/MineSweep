namespace ConsoleApp1.Models
{
    public class Game
    {
        public const string Alphabet = "ABCDEFGH";
        private const int MineProbability = 30;
        private Player _player { get; init; }
        private bool _isPlayerAlive => _player.RemainingLives > 0;
        private bool _isPlayerNotAtGoal => _player.CurrentPosition.Y != (FieldsPerDirection - 1);

        private ConsoleKey[] _allowedKeys =
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow,
        };

        public Game()
        {
            Mines = GenerateMineField();
            _player = new Player(0, 0);
        }

        public readonly int FieldsPerDirection = 8;
        public bool[,] Mines { get; init; }

        public void Start()
        {
            do
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"Current player position: {Alphabet[_player.CurrentPosition.X]}-{_player.CurrentPosition.Y}");
                Console.WriteLine($"Lifes left: {_player.RemainingLives}");
                Console.WriteLine($"Total moves: {_player.TotalMoves}");
                Console.WriteLine("---------------------------------------------");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (!_allowedKeys.Any(key => key == keyInfo.Key))
                {
                    Console.WriteLine("Error: Invalid key. Please press an arrow key.");
                    continue;
                }

                _player.Play(this, keyInfo.Key);
            }
            while (_isPlayerAlive && _isPlayerNotAtGoal);

            if (!_isPlayerAlive)
            {
                Console.WriteLine("You have no lifes left...");
                return;
            }

            Console.WriteLine("Success. You completed the quest...");
            Console.WriteLine($"Lifes: {_player.RemainingLives}");
            Console.WriteLine($"Total moves: {_player.TotalMoves}");

        }

        private bool[,] GenerateMineField()
        {
            var random = new Random();

            bool[,] mines = new bool[FieldsPerDirection, FieldsPerDirection];
            for (var i = 0; i < FieldsPerDirection; i++)
            {
                for (var j = FieldsPerDirection - 1; j > 0; j--)
                {
                    mines[i, j] = random.Next(0, 100) < MineProbability;
                }
            }

            return mines;
        }
    }
}
