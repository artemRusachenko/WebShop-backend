using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.DTOs.CategoryDtos;

namespace lesson1_Simple_Functions___Controller.DTOs.PoductsDtos
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<string> ImagesList { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int Popularity { get; set; }
        public GetCategoryDto Category { get; set; }
        public GetFilterDto Brand { get; set; }
    }
}
