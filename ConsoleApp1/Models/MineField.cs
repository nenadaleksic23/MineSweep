namespace Game.Models
{
    public class MineField(int X, int Y, bool hasMine)
    {
        public bool HasMineExploded { get; set; }
        public bool HasMine { get; init; } = hasMine;
        public int X { get; set; } = X;
        public int Y { get; set; } = Y;
    }
}
