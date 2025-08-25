using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;

/// <summary>
/// Profile for mapping between Cart entity and AddCartItem response/result.
/// </summary>
public class AddCartItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for AddCartItem operation.
    /// </summary>
    public AddCartItemProfile()
    {
        CreateMap<Cart, AddCartItemResult>();
        CreateMap<CartItem, CartItemResult>();
    }
}
