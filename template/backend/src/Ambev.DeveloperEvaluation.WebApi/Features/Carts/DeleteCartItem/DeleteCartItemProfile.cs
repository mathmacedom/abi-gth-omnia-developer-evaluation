using Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;

/// <summary>
/// Profile for mapping DeleteCartItem feature requests to commands
/// </summary>
public class DeleteCartItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCartItem feature
    /// </summary>
    public DeleteCartItemProfile()
    {
        CreateMap<DeleteCartItemRequest, DeleteCartItemCommand>();
    }
}
