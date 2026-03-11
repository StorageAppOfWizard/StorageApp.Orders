using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using StorageApp.Orders.Application.Contracts;
using System.Text.Json;

namespace StorageApp.Orders.Application
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IMessageConnection _connection;
        private readonly IMessageTopology _topology;
        private readonly IConfiguration _configuration;


        public MessageProducer(IMessageConnection connection, IConfiguration configuration, IMessageTopology topology)
        {
            _connection = connection;
            _configuration = configuration;
            _topology = topology;
        }

        public async Task SendMessage<T>(T message)
        {
            
            await using var channel = await _connection.GetConnection().CreateChannelAsync();
            await _topology.ConfigureAsync(channel);
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
