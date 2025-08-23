﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSale response/result.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CreateSaleItemCommand, SaleItem>();

        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItem, CreateSaleItemResult>();
    }
}
