using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Configuration;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}