using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;

/// <summary>
/// Profile for mapping between Product entity and GetProductResponse
/// </summary>
public class GetCartByCustomerIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetCartByCustomerIdProfile()
    {
        CreateMap<Cart, GetCartByCustomerIdResult>();
        CreateMap<CartItem, CartItemResult>();
    }
}
