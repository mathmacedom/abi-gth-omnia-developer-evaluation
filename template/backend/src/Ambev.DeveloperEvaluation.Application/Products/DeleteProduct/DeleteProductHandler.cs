using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductHandler> _logger;

    /// <summary>
    /// Initializes a new instance of DeleteProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="logger">The logger for DeleteProductHandler</param>
    public DeleteProductHandler(
        IProductRepository productRepository, 
        ILogger<DeleteProductHandler> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteProductCommand request
    /// </summary>
    /// <param name="request">The DeleteProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {DeleteProductCommand}...", nameof(DeleteProductCommand));

        var validator = new DeleteProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {DeleteProductCommand}", nameof(DeleteProductCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to delete request with ID {Id}...", request.Id);
        var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", request.Id);
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");
        }

        _logger.LogInformation("Handled {DeleteProductCommand} successfully...", nameof(DeleteProductCommand));
        return new DeleteProductResult { Success = true };
    }
}
