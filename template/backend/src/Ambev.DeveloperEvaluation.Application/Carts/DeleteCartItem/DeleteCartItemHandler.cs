using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;

/// <summary>
/// Handler for processing DeleteCartItemCommand requests
/// </summary>
public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, DeleteCartItemResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ILogger<DeleteCartItemHandler> _logger;

    /// <summary>
    /// Initializes a new instance of DeleteCartItemHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="logger">The logger for DeleteCartItemHandler</param>
    public DeleteCartItemHandler(
        ICartRepository cartRepository, 
        ILogger<DeleteCartItemHandler> logger)
    {
        _cartRepository = cartRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the DeleteCartItemCommand request
    /// </summary>
    /// <param name="request">The DeleteCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCartItemResult> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {DeleteCartItemCommand}...", nameof(DeleteCartItemCommand));

        var validator = new DeleteCartItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {DeleteCartItemCommand}", nameof(DeleteCartItemCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get cart with ID {Id}...", request.CartId);
        var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken);
        if (cart == null)
        {
            _logger.LogWarning("Cart with ID {CartId} not found", request.CartId);
            throw new KeyNotFoundException($"Cart with ID {request.CartId} not found");
        }

        _logger.LogInformation("Trying to get cart item with ID {Id}...", request.CartItemId);
        var cartItem = cart.Items.FirstOrDefault(i => i.Id == request.CartItemId);
        if (cartItem == null)
        {
            _logger.LogWarning("Cart Item with ID {CartItemId} not found", request.CartItemId);
            throw new KeyNotFoundException($"Cart Item with ID {request.CartItemId} not found");
        }

        cart.Items.Remove(cartItem);

        _logger.LogInformation("Updating cart ID {Id}...", request.CartId);
        await _cartRepository.UpdateAsync(cart, cancellationToken);

        _logger.LogInformation("Handled {DeleteCartItemCommand} successfully...", nameof(DeleteCartItemCommand));
        return new DeleteCartItemResult { Success = true };
    }
}
