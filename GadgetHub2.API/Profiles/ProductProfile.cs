using AutoMapper;
using GadgetHub2.API.DTOs.Product;
using GadgetHub2.API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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