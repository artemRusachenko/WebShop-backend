﻿namespace lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos
{
    public class GetOrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public GetProductDto? Product { get; set; }
    }
}
