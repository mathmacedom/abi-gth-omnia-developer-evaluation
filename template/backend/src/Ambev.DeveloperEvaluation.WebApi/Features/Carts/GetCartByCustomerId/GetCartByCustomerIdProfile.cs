using Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByCustomerId;

/// <summary>
/// Profile for mapping GetCartByCustomerId feature requests to commands
/// </summary>
public class GetCartByCustomerIdProfile : Profile
{
    public GetCartByCustomerIdProfile()
    {
        CreateMap<GetCartByCustomerIdRequest, GetCartByCustomerIdCommand>();
        CreateMap<GetCartByCustomerIdResult, GetCartByCustomerIdResponse>();
    }
}