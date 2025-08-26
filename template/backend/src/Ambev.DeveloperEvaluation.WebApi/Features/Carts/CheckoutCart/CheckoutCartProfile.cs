using Ambev.DeveloperEvaluation.Application.Carts.CheckoutCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CheckoutCart;

/// <summary>
/// Profile for mapping CheckoutCart feature requests to commands
/// </summary>
public class CheckoutCartProfile : Profile
{
    public CheckoutCartProfile()
    {
        CreateMap<CheckoutCartRequest, CheckoutCartCommand>();
    }
}