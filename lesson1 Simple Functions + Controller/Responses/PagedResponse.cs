namespace lesson1_Simple_Functions___Controller.Responses
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int ProductCount { get; set; }
        public double TotalPages { get; set; }
        public int TotalProducts { get; set; }

        public PagedResponse(T data, int pageNumber, int totalProducts)
        {
            PageNumber = pageNumber;
            ProductCount = 5;
            Data= data;
            IsSuccess = true;
            Error = null;
            TotalPages = totalProducts % 5 == 0 ? totalProducts / 5 : totalProducts / 5 + 1;
            TotalProducts = totalProducts;
        }
    }
}
