using Ambev.DeveloperEvaluation.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

/// <summary>
/// Handles SaleItemCancelled events.
/// </summary>
public class SaleItemCancelledConsumer : IConsumer<SaleItemCancelledEvent>
{
    private readonly ILogger<SaleItemCancelledConsumer> _logger;

    public SaleItemCancelledConsumer(ILogger<SaleItemCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SaleItemCancelledEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("SaleItemCancelled event received: SaleId={SaleId}, SaleNumber={SaleNumber}, ItemId={ItemId}, CancelledAt={CancelledAt}, FullPayload={FullPayload}",
            message.SaleId, message.SaleNumber, message.ItemId, message.CancelledAt, JsonSerializer.Serialize(message));

        await Task.CompletedTask;
    }
}
