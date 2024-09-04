namespace ConsoleApp1.Models
{
    public class Square
    {
        private bool _hasMineExploded { get; set; }
        public int X { get; init; }
        public int Y { get; init; }

        public bool CanNavigateToSquare()
        {
            return !_hasMineExploded;
        }
    }
}
