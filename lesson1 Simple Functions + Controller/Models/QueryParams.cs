namespace lesson1_Simple_Functions___Controller.Models
{
    public class QueryParams
    {
        public string? Name { get; set; }
        public string? CategoryId { get; set; }
        public string? SubCategoryId { get; set; }
        public string? SortingMethod { get; set; }
        public string? Brands { get; set; }
        public string? Colors { get; set; }
        public string? PriceRange { get; set; }
        public int PageNumber { get; set; }
        public int ProductCount { get; set; } = 5;

/*        public QueryParams()
        {
            PageNumber = 1;
        }*/
    }
}
