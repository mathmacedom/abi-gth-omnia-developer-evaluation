using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Handler for processing <see cref="CreateCartCommand"/> requests.
/// </summary>
public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateCartHandler"/>.
    /// </summary>
    /// <param name="cartRepository">The repository for managing carts.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    /// <param name="logger">The logger for CreateCartHandler</param>
    public CreateCartHandler(
        ICartRepository cartRepository, 
        IMapper mapper,
        ILogger<CreateCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateCartCommand"/> request.
    /// </summary>
    /// <param name="command">The create cart command containing the cart data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the created cart.</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CreateCartCommand}...", nameof(CreateCartCommand));
        
        var validator = new CreateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {CreateCartCommand}", nameof(CreateCartCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Creating cart...");
        var cart = _mapper.Map<Cart>(command);
        cart.UpdateStatus(CartStatus.Active);
        var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);
        var result = _mapper.Map<CreateCartResult>(createdCart);

        _logger.LogInformation("Handled {CreateCartCommand} successfully...", nameof(CreateCartCommand));
        return result;
    }
}
