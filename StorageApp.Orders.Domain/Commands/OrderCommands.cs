using StorageApp.Orders.Domain.Entity.MessagesEntity.Order;

namespace StorageApp.Orders.Domain.Commands
{
    //public record SendOrderCreated(OrderCreatedMessage message);
    public record SendOrderRemoved(OrderRemovedMessage message);
    public record SendOrderAccept(OrderAcceptedMessage message);
    public record SendOrderRejected(OrderRejectedMessage message);
}
