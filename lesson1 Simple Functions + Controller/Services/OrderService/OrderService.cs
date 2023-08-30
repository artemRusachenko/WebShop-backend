using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos;
using lesson1_Simple_Functions___Controller.DTOs.OrdersDtos;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace lesson1_Simple_Functions___Controller.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly PostgreSqlContext sqlServerContext;
        private readonly IMapper mapper;

        public OrderService(PostgreSqlContext context, IMapper mapper)
        {
            sqlServerContext = context;
            this.mapper = mapper;
        }

        public async Task<List<GetOrderDto>> GetOrders()
        {
            return await sqlServerContext.Orders.Select(o => mapper.Map<GetOrderDto>(o)).ToListAsync();
        }

        public async Task AddOrder(AddOrderDto order)
        {
            var user = sqlServerContext.Users.Find(order.UserId);

            Order newOrder = new Order();
            newOrder.TotalPrice = order.TotalPrice;
            newOrder.User = user; 
            sqlServerContext.Orders.Add(newOrder);

            foreach(var item in order.OrderItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.Quantity = item.Quantity;
                orderItem.Order = newOrder;
                orderItem.Product = sqlServerContext.Products.Find(item.ProductId);

                sqlServerContext.OrderItems.Add(orderItem);
            }
            await sqlServerContext.SaveChangesAsync();
        }

        public async Task<List<GetOrderDto>> GetUserOrders(int userId)
        {
            var orders = await sqlServerContext.Orders.Where(o => o.UserId == userId).ToListAsync();

            List<GetOrderDto> res = new();
            foreach (var order in orders)
            {
                var orderItems = sqlServerContext.OrderItems.Where(i => i.OrderId == order.Id).ToList();
                List<GetOrderItemDto> items = new List<GetOrderItemDto>();
                foreach (var item in orderItems)
                {
                    var product = await sqlServerContext.Products.FindAsync(item.ProductId);

                    GetOrderItemDto orderItem = new();
                    orderItem.Quantity = item.Quantity;
                    orderItem.Product = mapper.Map<GetProductDto>(product);
                    items.Add(orderItem);
                }
                GetOrderDto dto = new(order.Id, order.StateInfo, order.TotalPrice, order.CreatedDate, items);
                 res.Add(dto);
            }
            return res;
        }
    }
}
