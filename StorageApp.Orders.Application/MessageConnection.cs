using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using StorageApp.Orders.Application.Contracts;

namespace StorageApp.Orders.Application
{

    public class MessageConnection(IConnection connection) : IMessageConnection
    {
        const string exchangeName = "pedido.criado";
        const string queueName = "pedidos.criados";
        const string routingKey = "pedido.criado";


        private IConnection _connection = connection;
        private ConnectionFactory _factory;

        public void ConnectionMessage(IConfiguration configuration)
        {
                _factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:Host"],
                Port = int.Parse(configuration["RabbitMQ:Port"]),
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                VirtualHost = configuration["RabbitMQ:VirtualHost"],
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            };
        }

        public async Task InitializeAsync()
        {
            _connection = await _factory.CreateConnectionAsync();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

    }
}
