﻿
using DemoApp.Application.Features.Products.Commands.AddProduct;

namespace DemoApp.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AddProductCommand, Product>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.ProductName));
        CreateMap<Product, ProductResponse>();
    }
}
