namespace StorageApp.Orders.Domain.Entity.MessagesEntity.Product
{
    public class StockRejectedMessage
    {
        public Guid Id { get; set; }
        public bool StockAvailable { get; set; }

        public Guid OrderId { get; set; }
    }
}
