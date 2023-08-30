namespace lesson1_Simple_Functions___Controller.Models.Filters
{
    public class Brand: BaseEntity
    {
        public string? Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
