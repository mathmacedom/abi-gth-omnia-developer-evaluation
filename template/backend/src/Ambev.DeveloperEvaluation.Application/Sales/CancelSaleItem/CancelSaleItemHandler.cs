using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Handler for processing CancelSaleItemCommand requests
/// </summary>
public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBus _bus;
    private readonly ILogger<CancelSaleItemHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CancelSaleItemHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="bus">The Bus instance for publishing events.</param>
    /// <param name="logger">The logger for CancelSaleItemHandler</param>
    public CancelSaleItemHandler(
        ISaleRepository saleRepository,
        IBus bus,
        ILogger<CancelSaleItemHandler> logger)
    {
        _saleRepository = saleRepository;
        _bus = bus;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CancelSaleItemCommand request
    /// </summary>
    /// <param name="request">The CancelSaleItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<CancelSaleItemResponse> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CancelSaleItemCommand}...", nameof(CancelSaleItemCommand));

        var validator = new CancelSaleItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {CancelSaleItemCommand}", nameof(CancelSaleItemCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get sale with ID {Id}...", request.SaleId);
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale is null)
        {
            _logger.LogWarning("Sale with id {SaleId} not found", request.SaleId);
            throw new KeyNotFoundException("Sale not found");
        }

        _logger.LogInformation("Trying to get sale item with ID {Id}...", request.SaleItemId);
        var saleItem = sale.Items.FirstOrDefault(i => i.Id == request.SaleItemId);
        if (saleItem == null)
        {
            _logger.LogWarning("Sale Item with ID {SaleItemId} not found", request.SaleItemId);
            throw new KeyNotFoundException($"Sale Item with ID {request.SaleItemId} not found");
        }

        saleItem.Cancel();

        _logger.LogInformation("Updating sale ID {Id}...", request.SaleId);
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Handled {CancelSaleItemCommand} successfully...", nameof(CancelSaleItemCommand));

        try
        {
            _logger.LogInformation("Publishing event {SaleItemCancelledEvent} for sale ID {SaleId}...", nameof(SaleItemCancelledEvent), sale.Id);
            await _bus.Publish(new SaleItemCancelledEvent
            {
                SaleId = sale.Id,
                SaleNumber = sale.SaleNumber,
                ItemId = saleItem.Id,
                ProductId = saleItem.ProductId,
                ProductName = saleItem.ProductName,
                Quantity = saleItem.Quantity,
                Total = saleItem.Total,
                CancelledAt = sale.CancelledAt ?? DateTime.Now
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing event {SaleItemCancelledEvent} for sale ID {SaleId}", nameof(SaleItemCancelledEvent), sale.Id);
        }

        return new CancelSaleItemResponse { Success = true };
    }
}
