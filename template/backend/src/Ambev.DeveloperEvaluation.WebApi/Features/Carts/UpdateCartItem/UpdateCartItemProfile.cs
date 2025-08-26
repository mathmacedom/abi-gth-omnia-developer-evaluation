using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Profile for mapping UpdateProduct feature requests and responses
/// </summary>
public class UpdateCartItemProfile : Profile
{
    public UpdateCartItemProfile()
    {
        CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>();
        CreateMap<UpdateCartItemResult, UpdateCartItemResponse>();
    }
}
