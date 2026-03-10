using RabbitMQ.Client;

namespace StorageApp.Orders.Application.Contracts
{
    public interface IMessageConnection
    {
        IConnection GetConnection();
    }
}
