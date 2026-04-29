using MassTransit;
using RabbitMQ.Client;
using StorageApp.Orders.Application.Handlers.ConsumersHandler;
using StorageApp.Shared;
using StorageApp.Shared.Message.Order;


namespace StorageApp.Orders.Web.Configurations
{
    public static class MessageBrokerConfiguration
    {
        public static void AddMessageBrokerConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(busConfig =>
            {
                busConfig.UsingRabbitMq((context, config) =>
                {

                    config.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    config.Message<OrderCreatedMessage>(m =>
                    {
                        m.SetEntityName("order-created");

                    });

                    config.Publish<OrderCreatedMessage>(m =>
                    {
                        m.ExchangeType = ExchangeType.Fanout;

                    });

                    config.ConfigureEndpoints(context);

                });

                busConfig.AddConsumer<StockAcceptedMessageHandler>();
                busConfig.AddConsumer<StockRejectedMessageHandler>();
            });
        }
    }
}
