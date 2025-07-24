using AutoMapper;
using GadgetHub.API.Models;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Order;

namespace GadgetHub.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseDto>().ReverseMap();
        CreateMap<Order, CreateOrderDto>().ReverseMap();

        CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();

    }
}