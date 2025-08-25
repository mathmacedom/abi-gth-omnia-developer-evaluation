using Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;

/// <summary>
/// Profile for mapping AddCartItem feature requests and responses
/// </summary>
public class AddCartItemProfile : Profile
{
    public AddCartItemProfile()
    {
        CreateMap<AddCartItemRequest, AddCartItemCommand>();
        CreateMap<AddCartItemResult, AddCartItemResponse>();
        CreateMap<CartItemResult, CartItemResponse>();
    }
}
