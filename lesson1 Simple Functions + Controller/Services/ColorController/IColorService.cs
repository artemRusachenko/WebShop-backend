using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;

namespace lesson1_Simple_Functions___Controller.Services.ColorController
{
    public interface IColorService
    {
        public Task<List<GetFilterDto>> GetColors();
        public Task AddColor(AddFilterDto newColor);
    }
}
