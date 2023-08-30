using lesson1_Simple_Functions___Controller.DTOs.CategoryDtos;
using lesson1_Simple_Functions___Controller.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var result = await categoryService.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("{parentId}")]
        public async Task<ActionResult<List<GetCategoryDto>>> GetCategoryByParentId(int parentId)
        {
            var result = await categoryService.GetCategoryByParentId(parentId);
            return Ok(result);
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<List<GetProductDto>>> GetProductsByCategoryId(int id)
        {
            var result = await categoryService.GetProductsByCategoryId(id);
            if (result.Count == 0)
            {
                return NotFound("Category does have products");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCategoryDto>>> AddCategory(AddCategoryDto category)
        {
            var result = await categoryService.AddCategory(category);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<GetCategoryDto>>> UpdateCategory(UpdatedCategoryDto category)
        {
            var result = await categoryService.UpdateCategory(category);
            if(result == null)
                return NotFound("Category not found");
            return Ok(result);
        }
    }
}
