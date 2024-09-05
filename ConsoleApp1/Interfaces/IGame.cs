using Game.Models;

namespace Game.Interfaces
{
    public interface IGame
    {
        bool IsMoveValid(MineField nextPosition);
        bool ProcessMove(MineField nextPosition);
        bool IsQuestCompleted { get;}
        bool IsPlayerAlive { get;}
    }
}
