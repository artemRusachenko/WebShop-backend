using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.DTOs.CategoryDtos;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace lesson1_Simple_Functions___Controller.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;

        public CategoryService (PostgreSqlContext postgreSqlContext, IMapper mapper)
        {
            this.postgreSqlContext = postgreSqlContext;
            this.mapper = mapper;   
        }

        public async Task<List<GetCategoryDto>> AddCategory(AddCategoryDto category)
        {
            postgreSqlContext.Categories.Add(mapper.Map<Category>(category));
            await postgreSqlContext.SaveChangesAsync();
            return await postgreSqlContext.Categories.Select(c => mapper.Map<GetCategoryDto>(c)).ToListAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await postgreSqlContext.Categories.ToListAsync();
        }

        public async Task<List<GetCategoryDto>> GetCategoryByParentId(int parentId)
        {
            var categories = await postgreSqlContext.Categories.Where(c => c.ParentId == parentId).ToListAsync();
            return categories.Select(mapper.Map<GetCategoryDto>).ToList();
        }

        public async Task<List<GetProductDto>> GetProductsByCategoryId(int id)
        {
            var subCategoriesIds = await postgreSqlContext.Categories.Where(c => c.ParentId == id).Select(c => c.Id).ToListAsync();
            if (subCategoriesIds.Count > 0)
            {
                return postgreSqlContext.Products.Where(p => subCategoriesIds.Contains(p.CategoryId)).Select(p => mapper.Map<GetProductDto>(p)).ToList();
            }
            else
            {
                var result = postgreSqlContext.Products.Where(p => p.CategoryId == id).Select(p => mapper.Map<GetProductDto>(p)).ToList();
                return result;
            }
        }

        public async Task<GetCategoryDto?> UpdateCategory(UpdatedCategoryDto updatedCategory)
        {
            var category = await postgreSqlContext.Categories.FindAsync(updatedCategory.Id);
            if (category == null) 
                return null;

            category.Name = updatedCategory.Name;
            category.ParentId = updatedCategory.ParentId;
            await postgreSqlContext.SaveChangesAsync();
            return mapper.Map<GetCategoryDto>(category);
        }
    }
}
