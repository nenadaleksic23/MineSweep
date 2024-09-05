namespace Game.Models
{
    public class MineField(int X, int Y, bool hasMine) : Position(X, Y)
    {
        public bool HasMineExploded { get; set; }
        public bool HasMine { get; init; } = hasMine;

        public bool CanNavigateToMineField()
        {
            return !HasMineExploded;
        }
    }
}
