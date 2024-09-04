using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Game
    {
        public const string Alphabet = "ABCDEFGH";
        private const int MineProbability = 30;
        private Player _player { get; init; }
        private bool _isPlayerAlive => _player.RemainingLives > 0;
        private bool _isPlayerNotAtGoal => _player.CurrentPosition.Y != (FieldsPerDirection);

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

                var directions = GetDirectionInput();
                _player.Play(this, directions);
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

        private ConsoleKey GetDirectionInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (!_allowedKeys.Any(key => key == keyInfo.Key))
            {
                Console.WriteLine("Error: Invalid key. Please press an arrow key.");
            }

            return keyInfo.Key;
        }

        private bool[,] GenerateMineField()
        {
            var random = new Random();

            bool[,] mines = new bool[FieldsPerDirection, FieldsPerDirection];
            for (var i = 0; i < FieldsPerDirection; i++)
            {
                for (var j = 0; j < FieldsPerDirection; j++)
                {
                    mines[i, j] = random.Next(0, 100) < MineProbability;
                    if (mines[i, j])
                    {
                        Console.WriteLine($"Position of the Mine{i}: {i}{j}");
                    }
                }
            }

            return mines;
        }
    }
}
