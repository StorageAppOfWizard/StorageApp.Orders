using Ardalis.Result;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Application.Contracts
{
    public interface IOrderHandler
    {
        OrderStatus TargetStatus { get; }
        public Task<Result<Order>> Handle(Order order /*Product? product*/);
    }
}
