namespace Library_kursova.Helpers
{
    public class BookParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 2;

        private int _pageSize = 8;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Type { get; set; }

        public List<string> Categories { get; set; }
    }
}
