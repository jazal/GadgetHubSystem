using AutoMapper;
using GadgetHub.API.Models;
using GadgetHub.Dtos.Product;

namespace GadgetHub.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

        }
    }
}