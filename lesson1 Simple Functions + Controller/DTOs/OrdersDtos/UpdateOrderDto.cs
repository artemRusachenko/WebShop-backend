using lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos;
namespace lesson1_Simple_Functions___Controller.DTOs.OrdersDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public State State { get; set; }
        public decimal TotalPrice { get; set; }

        public List<AddOrderItemDto>? OrderItems { get; set; } = new();

    }
}
