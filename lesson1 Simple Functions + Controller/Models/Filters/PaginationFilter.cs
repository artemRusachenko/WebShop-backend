namespace lesson1_Simple_Functions___Controller.Models.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int ProductCount { get; set; } = 5;

        public PaginationFilter() 
        {
            PageNumber = 1;
        }

        public PaginationFilter(int pageNumber)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
        }
    }
}
