namespace StorageApp.Orders.Domain.Entity.MessagesEntity.Order
{
    public class OrderAcceptedMessage : General
    {
        public Guid ProductId { get; set; }
    }
}
