using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;

/// <summary>
/// Handler for processing <see cref="GetCartByCustomerIdCommand"/> requests
/// </summary>
public class GetCartByCustomerIdHandler : IRequestHandler<GetCartByCustomerIdCommand, GetCartByCustomerIdResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartByCustomerIdHandler> _logger;

    /// <summary>
    /// Initializes a new instance of GetCartByCustomerIdHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger for GetCartByCustomerIdHandler</param>
    public GetCartByCustomerIdHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<GetCartByCustomerIdHandler> logger)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the GetCartByCustomerIdCommand request
    /// </summary>
    /// <param name="request">The GetCartByCustomerId command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns> proByCustomerIducts found</returns>
    public async Task<GetCartByCustomerIdResult> Handle(GetCartByCustomerIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {GetCartByCustomerIdCommand}...", nameof(GetCartByCustomerIdCommand));

        var validator = new GetCartByCustomerIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {GetCartByCustomerIdCommand}", nameof(GetCartByCustomerIdCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Getting cart by customer ID {Id}...", request.CustomerId);
        _logger.LogInformation("Trying to get cart by customer ID {Id}...", request.CustomerId);
        var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (cart == null)
        {
            _logger.LogWarning("Cart with Customer ID {CustomerId} not found", request.CustomerId);
            throw new KeyNotFoundException($"Cart with Customer ID {request.CustomerId} not found");
        }

        _logger.LogInformation("Handled {GetCartByCustomerIdCommand} successfully...", nameof(GetCartByCustomerIdCommand));
        return _mapper.Map<GetCartByCustomerIdResult>(cart);
    }
}
