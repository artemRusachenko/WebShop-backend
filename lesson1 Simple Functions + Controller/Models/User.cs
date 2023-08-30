namespace lesson1_Simple_Functions___Controller.Models
{
    public class User: BaseEntity
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Password { get; set; } = "";

        public List<Order>? Orders { get; set; } = new();
        public List<Review>? Reviews { get; set; } = new();
        /*public Customer? Customer { get; set; }*/
    }
}
