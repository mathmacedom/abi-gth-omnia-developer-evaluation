using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;

/// <summary>
/// Handler for processing AddCartItemCommand requests
/// </summary>
public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, AddCartItemResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddCartItemHandler> _logger;

    /// <summary>
    /// Initializes a new instance of AddCartItemHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="logger">The logger for AddCartItemHandler</param>
    public AddCartItemHandler(
        ICartRepository cartRepository, 
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<AddCartItemHandler> logger)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the AddCartItemCommand request
    /// </summary>
    /// <param name="request">The AddCartItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the updated cart</returns>
    public async Task<AddCartItemResult> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {AddCartItemCommand}...", nameof(AddCartItemCommand));

        var validator = new AddCartItemCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {AddCartItemCommand}", nameof(AddCartItemCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get cart with ID {Id}...", request.CartId);
        var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken);
        if (cart == null)
        {
            _logger.LogWarning("Cart with ID {CartId} not found", request.CartId);
            throw new KeyNotFoundException($"Cart with ID {request.CartId} not found");
        }

        _logger.LogInformation("Trying to get product with ID {Id}...", request.ProductId);
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", request.ProductId);
            throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");
        }

        if (request.Quantity > 20)
        {
            _logger.LogWarning("Maximum 20 units per product allowed");
            throw new InvalidOperationException("Maximum 20 units per product allowed.");
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
        if (existingItem != null)
        {
            if (existingItem.Quantity + request.Quantity > 20)
            {
                _logger.LogWarning("Maximum 20 units per product allowed");
                throw new InvalidOperationException("Maximum 20 units per product allowed.");
            }

            existingItem.UpdateQuantity(existingItem.Quantity + request.Quantity);
        }
        else
        {
            cart.Items.Add(new CartItem(request.CartId, request.ProductId, product.Name, request.Quantity, product.UnitPrice));
        }

        _logger.LogInformation("Updating cart ID {Id}...", request.CartId);
        var updatedCart = await _cartRepository.UpdateAsync(cart, cancellationToken);
        var result = _mapper.Map<AddCartItemResult>(updatedCart);

        return result;
    }
}
