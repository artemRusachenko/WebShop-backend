using Microsoft.AspNetCore.Mvc;
using lesson1_Simple_Functions___Controller.Services.ProductService;
using lesson1_Simple_Functions___Controller.Models.Filters;
using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<GetProductDto>>>> GetProducts([FromQuery] PaginationFilter paginationFilter) 
        {
            var filter = new PaginationFilter(paginationFilter.PageNumber);
            var result = await productService.GetProducts(filter);
            return Ok((result));
        }

        [HttpGet("popular")]
        public async Task<ActionResult<List<GetProductDto>>> GetPopularProducts()
        {
            var result = await productService.GetPopularProducts();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<GetProductDto>> GetProductById(int id) 
        {
            var result = await productService.GetProductById(id);

            if (result == null)
                return NotFound("Product doesn't exist");

            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<List<GetProductDto>>> AddProduct (AddProductDto productData) 
        {
            var result = await productService.AddProduct(productData);
            return Ok(result);
        }

        [HttpPut]

        public async Task<ActionResult<GetProductDto?>> UpdateProduct (UpdateProductDto productData) 
        {
            var result = await productService.UpdateProduct(productData);
            if(result == null)
                return NotFound("Book doesn't exist");
            return Ok(result);
        } 

        [HttpGet("filter")]

        public async Task<ActionResult<List<GetProductDto>>> GetFilteredProducts([FromQuery] QueryParams filters/*, [FromQuery] PaginationFilter pFilters*/)
        {
           /* var pfilters = new PaginationFilter(pFilters.PageNumber);*/
            var result = await productService.GetFilteredProducts(filters/*, pfilters*/);
            return Ok(result);
        }

        [HttpGet("filters")]

        public async Task<ActionResult<Dictionary<string, List<GetFilterDto>>>> GetFilteredFilters([FromQuery] QueryParams filters)
        {
            var result = await productService.GetFilteredFilters(filters);
            return Ok(result);
        }

        /*[HttpDelete("{id}")]
        
        public async Task<ActionResult<Product>> DeleteProduct (int id) 
        {
            var result = await productService.DeleteProduct(id);

            if (result == null)
                return NotFound("Product not found");

            return Ok(result);
        }*/
    }
}