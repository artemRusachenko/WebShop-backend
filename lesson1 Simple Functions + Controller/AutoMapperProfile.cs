using AutoMapper;
using lesson1_Simple_Functions___Controller.DTOs.FiltersDtos;
using lesson1_Simple_Functions___Controller.DTOs.CategoryDtos;
using lesson1_Simple_Functions___Controller.DTOs.CustomersDtos;
using lesson1_Simple_Functions___Controller.DTOs.OrderItemsDtos;
using lesson1_Simple_Functions___Controller.DTOs.OrdersDtos;
using lesson1_Simple_Functions___Controller.Models.Filters;

namespace lesson1_Simple_Functions___Controller
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddFilterDto, Brand>();
            CreateMap<Brand, GetFilterDto>();

            CreateMap<AddFilterDto, Color>();
            CreateMap<Color, GetFilterDto>();

            CreateMap<AddCategoryDto, Category>();
            CreateMap<Category, GetCategoryDto>();

            CreateMap<OrderItem, GetOrderItemDto>();
            CreateMap<AddOrderItemDto, OrderItem>();

            CreateMap<Order, GetOrderDto>();
            CreateMap<AddOrderDto, Order>();

            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();

            CreateMap<SignUpUserDto, User>();
            CreateMap<User, GetUserDto>();

        }
    }
}
