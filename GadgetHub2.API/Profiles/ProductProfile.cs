using AutoMapper;
using GadgetHub.Dtos.Product;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Profiles
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