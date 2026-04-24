namespace StorageApp.Orders.Domain.Entity.MessagesEntity.Order
{
    public class OrderCreatedMessage : General
    {
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
