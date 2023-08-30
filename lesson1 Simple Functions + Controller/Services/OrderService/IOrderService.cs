using lesson1_Simple_Functions___Controller.DTOs.OrdersDtos;

namespace lesson1_Simple_Functions___Controller.Services.OrderService
{
    public interface IOrderService
    {
        public Task<List<GetOrderDto>> GetOrders();
        public Task AddOrder(AddOrderDto order);
        public Task<List<GetOrderDto>> GetUserOrders(int userId);
        //public Task<List<GetProductDto>?> GetProductsById(int id);
        //public Task<GetProductDto> UpdateOrderDto(UpdateOrderDto order);
        /*public Task<List<GetOrderDto>?> CompleteOrder(int id);*/
    }
}