using lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos;
namespace lesson1_Simple_Functions___Controller.DTOs.OrdersDtos
{
    public class AddOrderDto
    {
        public decimal TotalPrice { get; set; }

        public List<AddOrderItemDto> OrderItems { get; set; }

        public int UserId { get; set; }
    }
}