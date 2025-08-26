using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBus _bus;
    private readonly ILogger<CancelSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="logger">The logger for CancelSaleHandler</param>
    public CancelSaleHandler(
        ISaleRepository saleRepository,
        IBus bus,
        ILogger<CancelSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _bus = bus;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    /// <param name="request">The CancelSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<CancelSaleResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CancelSaleCommand}...", nameof(CancelSaleCommand));

        var validator = new CancelSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        _logger.LogInformation("Checking if request is valid...");
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {CancelSaleCommand}", nameof(CancelSaleCommand));
            throw new ValidationException(validationResult.Errors);
        }

        _logger.LogInformation("Trying to get sale with ID {Id}...", request.Id);
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
        {
            _logger.LogWarning("Sale with id {SaleId} not found", request.Id);
            throw new KeyNotFoundException("Sale not found");
        }

        _logger.LogInformation("Trying to cancel request with ID {Id}...", request.Id);
        var success = await _saleRepository.CancelAsync(request.Id, cancellationToken);
        if (!success)
        {
            _logger.LogWarning("Sale with ID {SaleId} not found", request.Id);
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        }

        _logger.LogInformation("Handled {CancelSaleCommand} successfully...", nameof(CancelSaleCommand));

        try
        {
            _logger.LogInformation("Publishing event {SaleCancelledEvent} for sale ID {SaleId}...", nameof(SaleCancelledEvent), sale.Id);
            await _bus.Publish(new SaleCancelledEvent
            {
                SaleId = sale.Id,
                SaleNumber = sale.SaleNumber,
                CustomerId = sale.CustomerId,
                CustomerName = sale.CustomerName,
                CancelledAt = sale.CancelledAt ?? DateTime.Now
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing event {SaleCancelledEvent} for sale ID {SaleId}", nameof(SaleCancelledEvent), sale.Id);
        }

        return new CancelSaleResponse { Success = true };
    }
}
