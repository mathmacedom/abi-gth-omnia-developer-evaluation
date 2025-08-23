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
    /// <param name="logger">The logger for UpdateSaleHandler</param>
    public UpdateSaleHandler(
        ISaleRepository saleRepository, 
        IMapper mapper, 
        ILogger<UpdateSaleHandler> logger)
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

        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        _logger.LogInformation("Trying to get sale with ID {Id}...", command.Id);
        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

        if (sale is null)
        {
            _logger.LogWarning("Sale with id {SaleId} not found", command.Id);
            throw new KeyNotFoundException("Sale not found");
        }

        var items = _mapper.Map<List<SaleItem>>(command.Items);
        sale.UpdateSale(command.BranchId, command.CustomerId, items);

        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        var result = _mapper.Map<UpdateSaleResult>(updatedSale);
        
        return result;
    }
}
