using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing <see cref="UpdateProductCommand"/> requests.
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductHandler> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateProductHandler"/>.
    /// </summary>
    /// <param name="productRepository">The repository for managing products.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    /// <param name="logger">The logger for UpdateProductHandler</param>
    public UpdateProductHandler(
        IProductRepository productRepository, 
        IMapper mapper, 
        ILogger<UpdateProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateProductCommand"/> request.
    /// </summary>
    /// <param name="request">The create product command containing the product data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the created product.</returns>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("The handler {UpdateProductHandler} was triggered to handle {UpdateProductCommand}", nameof(UpdateProductHandler), nameof(UpdateProductCommand));

        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {UpdateProductCommand}", nameof(UpdateProductCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get product with ID {Id}...", request.Id);
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            _logger.LogWarning("Product with id {ProductId} not found", request.Id);
            throw new KeyNotFoundException("Product not found");
        }

        _mapper.Map(request, product);

        var updatedProduct = await _productRepository.UpdateAsync(product, cancellationToken);
        var result = _mapper.Map<UpdateProductResult>(updatedProduct);
        
        return result;
    }
}
