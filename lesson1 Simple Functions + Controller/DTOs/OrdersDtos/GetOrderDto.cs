using lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos;
namespace lesson1_Simple_Functions___Controller.DTOs.OrdersDtos
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string StateInfo { get; set; }
        public decimal TotalPrice { get; set; }
        private DateTime CreatedDate { get; set; }
        public string CreationDate
        {
            get
            {
                return CreatedDate.Date.ToString().Substring(0, 10);
            }
        }

        public List<GetOrderItemDto> OrderItems { get; set; } = new();

        public GetOrderDto(int id, string stateInfo, decimal totalPrice, DateTime createdDate, List<GetOrderItemDto> orderItems) 
        {
            Id = id;
            StateInfo = stateInfo;
            TotalPrice = totalPrice;
            CreatedDate = createdDate;
            OrderItems = orderItems;
        }


    }
}
