using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing <see cref="CreateProductCommand"/> requests.
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductHandler> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateProductHandler"/>.
    /// </summary>
    /// <param name="productRepository">The repository for managing products.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    /// <param name="logger">The logger for CreateProductHandler</param>
    public CreateProductHandler(
        IProductRepository productRepository, 
        IMapper mapper,
        ILogger<CreateProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateProductCommand"/> request.
    /// </summary>
    /// <param name="command">The create product command containing the product data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the created product.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CreateProductCommand}...", nameof(CreateProductCommand));
        
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {CreateProductCommand}", nameof(CreateProductCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Creating product...");
        var product = _mapper.Map<Product>(command);
        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);

        _logger.LogInformation("Handled {CreateProductCommand} successfully...", nameof(CreateProductCommand));
        return result;
    }
}
