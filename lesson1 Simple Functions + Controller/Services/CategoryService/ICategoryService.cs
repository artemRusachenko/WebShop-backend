using lesson1_Simple_Functions___Controller.DTOs.CategoryDtos;

namespace lesson1_Simple_Functions___Controller.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<List<GetCategoryDto>> AddCategory(AddCategoryDto category);
        public Task<GetCategoryDto?> UpdateCategory(UpdatedCategoryDto category);
        public Task<List<GetProductDto>> GetProductsByCategoryId(int id);
        public Task<List<GetCategoryDto>> GetCategoryByParentId(int parentId);
    }
}
