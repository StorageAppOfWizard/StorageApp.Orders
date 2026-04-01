using MassTransit;
using StorageApp.Orders.Domain.Contracts;

namespace StorageApp.Orders.Web
{
    public class MessagePublisher(IBus bus) : IMessagePublisher
    {
        public async Task SendMessage<T>(T message, CancellationToken cancellationToken = default)
        {
            await bus.Publish(message, cancellationToken);
        }
    }
}
