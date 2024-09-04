namespace ConsoleApp1.Models
{
    public class Square(int X, int Y, bool hasMine) : Position(X, Y)
    {
        private bool _hasMineExploded { get; set; }
        public bool HasMine { get; init; } = hasMine;

        public bool CanNavigateToSquare()
        {
            return !_hasMineExploded;
        }
    }
}
