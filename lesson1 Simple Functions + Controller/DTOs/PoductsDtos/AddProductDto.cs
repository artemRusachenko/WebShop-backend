namespace lesson1_Simple_Functions___Controller.DTOs.PoductsDtos
{
    public class AddProductDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Images { get; set; }
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
