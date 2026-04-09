namespace StorageApp.Orders.Domain.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt{ get; set; } = DateTime.UtcNow;



        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }

    }
}
