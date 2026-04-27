using Ardalis.Result;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Application.Handlers
{
    public class PendingHandler : IOrderHandler
    {
        public OrderStatus TargetStatus => OrderStatus.Pending;

        public async Task<Result<Order>> Handle(Order order)
        {
            if (order.Status != OrderStatus.Processing)
            {
                return Result.Error("Just pending order can be approved");
            }
            order.UpdateStatus(OrderStatus.Pending);

            return Result.Success(order);
        }
    }
}
