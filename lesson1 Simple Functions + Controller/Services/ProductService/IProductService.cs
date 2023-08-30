using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Models;
using lesson1_Simple_Functions___Controller.Models.Filters;
using lesson1_Simple_Functions___Controller.Responses;

namespace lesson1_Simple_Functions___Controller.Services.ProductService
{
    public interface IProductService
    {
        Task<PagedResponse<List<GetProductDto>>> GetProducts(PaginationFilter filter);
        Task<List<GetProductDto>> GetPopularProducts();
        Task<GetProductDto?> GetProductById(int id);
        Task<List<GetProductDto>> AddProduct(AddProductDto product);
        Task<GetProductDto?> UpdateProduct(UpdateProductDto updatedProduct);
        Task<PagedResponse<List<GetProductDto>>> GetFilteredProducts(QueryParams filters/*, PaginationFilter pFilters*/);
        Task<Dictionary<string, List<GetFilterDto>>> GetFilteredFilters(QueryParams filters);
        //Task<List<GetProductDto>?> DeleteProduct(int id);
    }
}
