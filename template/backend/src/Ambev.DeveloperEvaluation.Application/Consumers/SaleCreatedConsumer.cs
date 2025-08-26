using Ambev.DeveloperEvaluation.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

/// <summary>
/// Handles SaleCreated events.
/// </summary>
public class SaleCreatedConsumer : IConsumer<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedConsumer> _logger;

    public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SaleCreatedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("SaleCreated event received: CartId={CartId}, SaleId={SaleId}, SaleNumber={SaleNumber}, Customer={Customer}, Total={Total}, FullPayload={FullPayload}",
            message.CartId, message.SaleId, message.SaleNumber, message.CustomerName, message.TotalAmount, JsonSerializer.Serialize(message));

        await Task.CompletedTask;
    }
}
