using Ambev.DeveloperEvaluation.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

/// <summary>
/// Handles SaleCancelled events.
/// </summary>
public class SaleCancelledConsumer : IConsumer<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledConsumer> _logger;

    public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SaleCancelledEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("SaleCancelled event received: SaleId={SaleId}, SaleNumber={SaleNumber}, CustomerId={CustomerId}, CancelledAt={CancelledAt}, FullPayload={FullPayload}",
            message.SaleId, message.SaleNumber, message.CustomerId, message.CancelledAt, JsonSerializer.Serialize(message));

        await Task.CompletedTask;
    }
}
