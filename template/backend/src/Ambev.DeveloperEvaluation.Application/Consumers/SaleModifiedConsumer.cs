using Ambev.DeveloperEvaluation.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Consumers;

/// <summary>
/// Handles SaleModified events.
/// </summary>
public class SaleModifiedConsumer : IConsumer<SaleModifiedEvent>
{
    private readonly ILogger<SaleModifiedConsumer> _logger;

    public SaleModifiedConsumer(ILogger<SaleModifiedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SaleModifiedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("SaleModified event received: SaleId={SaleId}, ModifiedAt={ModifiedAt}, FullPayload={FullPayload}",
            message.SaleId, message.ModifiedAt, JsonSerializer.Serialize(message));

        await Task.CompletedTask;
    }
}
