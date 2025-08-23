using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing <see cref="CreateSaleCommand"/> requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleHandler"/>.
    /// </summary>
    /// <param name="saleRepository">The repository for managing sales.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    /// <param name="logger">The logger for CreateSaleHandler</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository, 
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="CreateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The create sale command containing the sale data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the created sale.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CreateSaleCommand}...", nameof(CreateSaleCommand));
        
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Creating sale...");
        var sale = _mapper.Map<Sale>(command);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);

        _logger.LogInformation("Handled {CreateSaleCommand} successfully...", nameof(CreateSaleCommand));
        return result;
    }
}
