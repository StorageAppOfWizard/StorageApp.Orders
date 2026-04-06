namespace StorageApp.Orders.Domain.Entity
{
    public class OrderMessage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
