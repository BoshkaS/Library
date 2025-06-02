namespace Library.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currenPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currenPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
