using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Models.Filters;

namespace lesson1_Simple_Functions___Controller.Services.BrandService
{
    public interface IBrandService
    {
        public Task<List<GetFilterDto>> GetBrands();
        public Task AddBrand(AddFilterDto newBrand);
    }
}
