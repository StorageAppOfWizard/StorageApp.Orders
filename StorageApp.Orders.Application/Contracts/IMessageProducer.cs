using Microsoft.Extensions.Configuration;

namespace StorageApp.Orders.Application.Contracts
{
    public interface IMessageProducer
    {
        public Task SendMessage<T>(T message);
    }
}
