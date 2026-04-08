using MassTransit;
using RabbitMQ.Client;
using StorageApp.Orders.Domain.Entity;

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

                    config.Message<OrderMessage>(m =>
                    {
                        m.SetEntityName("order-created");

                    });

                    config.Publish<OrderMessage>(m =>
                    {
                        m.ExchangeType = ExchangeType.Fanout;
                    });

                    config.ConfigureEndpoints(context);

                });
            });
        }
    }
}
