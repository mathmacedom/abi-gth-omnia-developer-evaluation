using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CheckoutCart;

/// <summary>
/// Handler for processing <see cref="CheckoutCartCommand"/> requests.
/// </summary>
public class CheckoutCartHandler : IRequestHandler<CheckoutCartCommand, GetSaleResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckoutCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="CheckoutCartHandler"/>.
    /// </summary>
    /// <param name="cartRepository">The repository for managing carts.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    /// <param name="logger">The logger for CheckoutCartHandler</param>
    public CheckoutCartHandler(
        ICartRepository cartRepository,
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CheckoutCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CheckoutCartCommand"/> request.
    /// </summary>
    /// <param name="request">The create cart command containing the cart data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the checked out cart.</returns>
    public async Task<GetSaleResult> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CheckoutCartCommand}...", nameof(CheckoutCartCommand));
        
        var validator = new CheckoutCartCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {CheckoutCartCommand}", nameof(CheckoutCartCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get cart with ID {Id}...", request.Id);
        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart == null)
        {
            _logger.LogWarning("Cart with ID {CartId} not found", request.Id);
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found");
        }

        if (cart.Status != CartStatus.Active)
        {
            _logger.LogWarning("The cart with ID {CartId} isn't active", request.Id);
            throw new KeyNotFoundException($"Cart with ID {request.Id} is not active to be checked out");
        }

        if (cart.Items.Count == 0)
        {
            _logger.LogWarning("The cart with ID {CartId} is empty", request.Id);
            throw new KeyNotFoundException($"Cart with ID {request.Id} is empty");
        }

        foreach (var item in cart.Items)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException($"Product {item.ProductName} exceeds max allowed (20 units)");
        }

        _logger.LogInformation("Creating a sale...");
        var sale = new Sale(SaleStatus.Open, cart.CustomerId, cart.Branch);
        var saleItems = new List<SaleItem>();
        _logger.LogInformation("Add items to new sale...");
        foreach (var item in cart.Items)
        {
            saleItems.Add(new SaleItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice));
        }
        
        sale.AddItems(saleItems);

        _logger.LogInformation("Setting cart status to converted...");
        cart.UpdateStatus(CartStatus.Converted);

        _logger.LogInformation("Persisting the new sale...");
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        
        _logger.LogInformation("Updating the cart...");
        await _cartRepository.UpdateAsync(cart, cancellationToken);

        var result = _mapper.Map<GetSaleResult>(createdSale);

        _logger.LogInformation("Handled {CheckoutCartCommand} successfully...", nameof(CheckoutCartCommand));
        return result;
    }
}
