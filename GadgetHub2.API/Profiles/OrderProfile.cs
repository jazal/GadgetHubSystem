using AutoMapper;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Order;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseDto>().ReverseMap();
        CreateMap<Order, CreateOrderDto>().ReverseMap();

        CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();

    }
}