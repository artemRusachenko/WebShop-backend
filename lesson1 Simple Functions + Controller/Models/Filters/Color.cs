namespace lesson1_Simple_Functions___Controller.Models.Filters
{
    public class Color : BaseEntity
    {
        public string? Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
