using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.Models.Filters;

namespace lesson1_Simple_Functions___Controller.Services.ColorController
{
    public class ColorService : IColorService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;

        public ColorService(PostgreSqlContext postgreSqlContext, IMapper mapper)
        {
            this.postgreSqlContext = postgreSqlContext;
            this.mapper = mapper;
        }

        public async Task AddColor(AddFilterDto newColor)
        {
            postgreSqlContext.Add(mapper.Map<Color>(newColor));
            await postgreSqlContext.SaveChangesAsync();
        }

        public async Task<List<GetFilterDto>> GetColors()
        {
            return await postgreSqlContext.Colors.Select(c => mapper.Map<GetFilterDto>(c)).ToListAsync();
        }
    }
}
