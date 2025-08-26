using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Handler for processing <see cref="GetAllProductsCommand"/> requests
/// </summary>
public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, GetAllProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllProductsHandler> _logger;

    /// <summary>
    /// Initializes a new instance of GetAllProductsHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger for GetAllProductsHandler</param>
    public GetAllProductsHandler(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<GetAllProductsHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the GetAllProductsCommand request
    /// </summary>
    /// <param name="request">The GetAllProducts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All products found</returns>
    public async Task<GetAllProductsResult> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {GetAllProductsCommand}...", nameof(GetAllProductsCommand));

        var validator = new GetAllProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Getting all products...");
        var products = await _productRepository.GetAllAsync(request.ActiveOnly, request.Category, cancellationToken);

        _logger.LogInformation("Handled {GetAllProductsCommand} successfully...", nameof(GetAllProductsCommand));
        return _mapper.Map<GetAllProductsResult>(products);
    }
}
