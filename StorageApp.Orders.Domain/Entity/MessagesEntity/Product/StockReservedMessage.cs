namespace StorageApp.Orders.Domain.Entity.MessagesEntity.Product
{
    public class StockReservedMessage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public bool StockAvailable { get; set; }
    }
}
