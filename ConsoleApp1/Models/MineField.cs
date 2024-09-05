namespace Game.Models
{
    public class MineField(int X, int Y, bool hasMine)
    {
        private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public bool HasMineExploded { get; set; }
        public bool HasMine { get; init; } = hasMine;
        public int X { get; set; } = X;
        public int Y { get; set; } = Y;
        public string UIPositionName
        {
            get => $"{alphabet[X]}{Y + 1}";
        }
    }
}
