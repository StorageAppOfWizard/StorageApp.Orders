using RabbitMQ.Client;
using 

namespace StorageApp.Orders.Application
{
    public class MessageProducer
    {
        const string exchangeName = "pedido.criado";
        const string queueName = "pedidos.criados";
        const string routingKey = "pedido.criado";
    

        public static async Task Connect()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            };


            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync
            (
                exchange: exchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null
            );

            await channel.QueueDeclareAsync
            (
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            await channel.QueueBindAsync
            (
                queue: queueName,
                exchange: exchangeName,
                routingKey: routingKey,
                arguments: null
            );

        }

       
    }
}
