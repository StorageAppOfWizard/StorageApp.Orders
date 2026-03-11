using RabbitMQ.Client;

namespace StorageApp.Orders.Application.Contracts
{
    public interface IMessageTopology
    {
        public Task ConfigureAsync(IChannel channel);
    }
}
