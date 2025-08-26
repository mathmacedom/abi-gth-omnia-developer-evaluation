using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Handler for processing <see cref="GetCartCommand"/> requests
/// </summary>
public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of GetCartHandler
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger for GetCartHandler</param>
    public GetCartHandler(
        ICartRepository cartRepository,
        IMapper mapper,
        ILogger<GetCartHandler> logger)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the GetCartCommand request
    /// </summary>
    /// <param name="request">The GetCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    public async Task<GetCartResult> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {GetCartCommand}...", nameof(GetCartCommand));

        var validator = new GetCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {GetCartCommand}", nameof(GetCartCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get cart with ID {Id}...", request.Id);
        var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart == null)
        {
            _logger.LogWarning("Cart with ID {CartId} not found", request.Id);
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found");
        }

        _logger.LogInformation("Handled {GetCartCommand} successfully...", nameof(GetCartCommand));
        return _mapper.Map<GetCartResult>(cart);
    }
}
