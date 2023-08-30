namespace lesson1_Simple_Functions___Controller.Models
{
    public class Review: BaseEntity
    {
        public string Text { get; set; } = "";
        public int Rating { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
