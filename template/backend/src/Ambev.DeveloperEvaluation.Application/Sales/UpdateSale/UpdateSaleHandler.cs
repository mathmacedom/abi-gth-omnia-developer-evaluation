using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing <see cref="UpdateSaleCommand"/> requests.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateSaleHandler"/>.
    /// </summary>
    /// <param name="saleRepository">The repository for managing sales.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the <see cref="UpdateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The create sale command containing the sale data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the created sale.</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("The handler {UpdateSaleHandler} was triggered to handle {UpdateSaleCommand}", nameof(UpdateSaleHandler), nameof(UpdateSaleCommand));

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

        if (sale is null)
        {
            _logger.LogWarning("Sale with id {SaleId} not found", command.Id);
            throw new KeyNotFoundException("Sale not found");
        }

        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Map command to domain entity
        var sale = _mapper.Map<Sale>(command);

        // Persist sale
        var createdSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Map result to response DTO
        return _mapper.Map<UpdateSaleResult>(createdSale);
    }
}
