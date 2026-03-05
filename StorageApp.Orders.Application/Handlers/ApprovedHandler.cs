using Ardalis.Result;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Application.Handlers
{
    public class ApprovedHandler : IOrderHandler
    {
        public OrderStatus TargetStatus => OrderStatus.Approved;

        public async Task<Result<Order>> Handle(Order order, Product product)
        {
            if (order.Status != OrderStatus.Pending)
            {
                return Result.Error("Just pending order can be approved");
            }
            order.UpdateStatus(OrderStatus.Approved);

            return Result.Success(order);
        }
    }
}
