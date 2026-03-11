using Ardalis.Result;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Domain.Entity;

namespace StorageProject.Application.Handlers
{
    public class RejectHandler : IOrderHandler

    {
        public OrderStatus TargetStatus => OrderStatus.Reject;

        public async Task<Result<Order>> Handle(Order order /*Product? product*/)
        {

            if (order.Status != OrderStatus.Pending)
                return Result.Error("Just pending order can be rejected");

            //await order.RestoreProductStock(product);
            order.UpdateStatus(OrderStatus.Reject);

            return Result.Success(order);
        }
    }
}
