using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Models.Filters;

namespace lesson1_Simple_Functions___Controller.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;

        public BrandService (PostgreSqlContext postgreSqlContext, IMapper mapper)
        {
            this.postgreSqlContext = postgreSqlContext;
            this.mapper = mapper;
        }

        public async Task AddBrand(AddFilterDto newBrand)
        {
            postgreSqlContext.Add(mapper.Map<Brand>(newBrand));
            await postgreSqlContext.SaveChangesAsync();
        }

        public async Task<List<GetFilterDto>> GetBrands()
        {
            return await postgreSqlContext.Brands.Select(b => mapper.Map<GetFilterDto>(b)).ToListAsync();
        }
    }
}
