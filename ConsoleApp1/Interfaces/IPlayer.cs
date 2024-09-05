using Game.Models;

namespace Game.Interfaces
{
    public interface IPlayer
    {
        void Play(IGame game, ConsoleKey direction); 
        void LoseLife();
        MineField CurrentPosition { get; }
        int RemainingLives { get; }
        int TotalMoves { get; }
    }
}
