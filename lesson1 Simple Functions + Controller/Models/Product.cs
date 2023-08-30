using lesson1_Simple_Functions___Controller.Models.Filters;

namespace lesson1_Simple_Functions___Controller.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Images { get; set; } = "";
        public string Description { get; set; } = "";
        public int Popularity { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set;}

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int ColorId { get; set; }
        public Color? Color { get; set; }

        public List<OrderItem>? OrderItems { get; set; } = new();
        public List<Review>? Reviews { get; set; } = new();


        public List<string> ImagesList
        {
            get
            {
                return this.Images.Split(';').ToList();
            }
        }
    }
}
