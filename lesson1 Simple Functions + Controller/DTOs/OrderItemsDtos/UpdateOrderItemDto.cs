namespace lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos
{
    public class UpdateOrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
