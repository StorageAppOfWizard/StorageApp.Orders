using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using StorageApp.Orders.Application.Contracts;
using System.Text.Json;

namespace StorageApp.Orders.Application
{
    public class MessageProducer
    {
        private readonly IMessageConnection _connection;
        private readonly IConfiguration _configuration;
        public async Task SendMessage<T>(T message)
        {
            await using var channel = await _connection.GetConnection().CreateChannelAsync();
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
                ContentEncoding = "utf-8"
            };

            await channel.BasicPublishAsync
             (
                exchange: _configuration["RabbitMqEnviroment:ExchangeName"],
                routingKey: _configuration["RabbitMqEnviroment:RoutingKey"],
                basicProperties: properties,
                mandatory: false,
                body: body

              );


        }
    }
}
