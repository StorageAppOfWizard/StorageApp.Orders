using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Application.DTO
{
    public record UpdateOrderDTO : CreateOrderDTO
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
