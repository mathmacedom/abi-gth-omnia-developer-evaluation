using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Handler for processing UpdateCartItemCommand requests
/// </summary>
public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, UpdateCartItemResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCartItemHandler> _logger;

    /// <summary>
    /// Initializes a new instance of UpdateCartItemHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="logger">The logger for UpdateCartItemHandler</param>
    public UpdateCartItemHandler(
        ICartRepository cartRepository, 
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<UpdateCartItemHandler> logger)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the UpdateCartItemCommand request
    /// </summary>
    /// <param name="request">The UpdateCartItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the updated cart</returns>
    public async Task<UpdateCartItemResult> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {UpdateCartItemCommand}...", nameof(UpdateCartItemCommand));

        var validator = new UpdateCartItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {UpdateCartItemCommand}", nameof(UpdateCartItemCommand));
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

        if (request.Quantity > 20)
        {
            _logger.LogWarning("Maximum 20 units per product allowed");
            throw new InvalidOperationException("Maximum 20 units per product allowed.");
        }

        cartItem.UpdateQuantity(request.Quantity);

        _logger.LogInformation("Updating cart ID {Id}...", request.CartId);
        var updatedCart = await _cartRepository.UpdateAsync(cart, cancellationToken);
        var result = _mapper.Map<UpdateCartItemResult>(updatedCart);

        return result;
    }
}
