using lesson1_Simple_Functions___Controller.DTOs.OrdersDtos;
using lesson1_Simple_Functions___Controller.Models;
using lesson1_Simple_Functions___Controller.Services.OrderService;
using lesson1_Simple_Functions___Controller.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await orderService.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Order>>> GetUserOrders(int userId)
        {
            var orders = await orderService.GetUserOrders(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder(AddOrderDto order) 
        {
            await orderService.AddOrder(order);
            return Ok();
        }
        
        /*[HttpPut("{id}")]
        public async Task<ActionResult<Order>> CompleteOrder(int id)
        {
            var order = await orderService.CompleteOrder(id);
            if (order == null)
            {
                return NotFound("Order doesn't exist");
            }
            return Ok(order);
        }*/
    }
}