using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Profile for mapping between Cart entity and UpdateCartItem response/result.
/// </summary>
public class UpdateCartItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCartItem operation.
    /// </summary>
    public UpdateCartItemProfile()
    {
        CreateMap<Cart, UpdateCartItemResult>();
        CreateMap<CartItem, CartItemResult>();
    }
}
