using Microsoft.AspNetCore.Builder;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Ambev.DeveloperEvaluation.Application.Consumers;

namespace Ambev.DeveloperEvaluation.IoC
{
    public static class DependencyBuilder
    {
        public static void AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(t =>
            {
                var connectionString = builder.Configuration.GetConnectionString("RabbitMQ");

                t.AddConsumer<SaleCreatedConsumer>();
                t.AddConsumer<SaleModifiedConsumer>();
                t.AddConsumer<SaleCancelledConsumer>();
                t.AddConsumer<SaleItemCancelledConsumer>();

                t.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(connectionString!), host =>
                    {
                        host.Username("developer");
                        host.Password("ev@luAt10n");
                    });

                    cfg.ReceiveEndpoint("sale-created-queue", e =>
                    {
                        e.ConfigureConsumer<SaleCreatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("sale-modified-queue", e =>
                    {
                        e.ConfigureConsumer<SaleModifiedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("sale-cancelled-queue", e =>
                    {
                        e.ConfigureConsumer<SaleCancelledConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("saleitem-cancelled-queue", e =>
                    {
                        e.ConfigureConsumer<SaleItemCancelledConsumer>(context);
                    });
                });
            });
        }
    }
}
