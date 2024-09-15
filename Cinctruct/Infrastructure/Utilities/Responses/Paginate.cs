namespace Infrastructure.Utilities.Responses
{
    public class Paginate<T>
    {
        public int Size { get; set; }
        public int Index { get; set; }
        public int Pages { get; set; }
        public long Count { get; set; }
        public List<T>? Items { get; set; }
        public bool HasPrevious => Index > 0;
        public bool HasNext => Index + 1 < Pages;
    }
}
