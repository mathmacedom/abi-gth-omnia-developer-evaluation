using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Profile for mapping between Product entity and GetProductResponse
/// </summary>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<List<Product>, GetAllProductsResult>()
            .ForPath(dest => dest.Products, opt => opt.MapFrom(src => src));
    }
}
