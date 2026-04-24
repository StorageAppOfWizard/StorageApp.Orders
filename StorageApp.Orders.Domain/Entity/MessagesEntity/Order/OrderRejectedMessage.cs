namespace StorageApp.Orders.Domain.Entity.MessagesEntity.Order
{
    public class OrderRejectedMessage 
    {
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
    }
}
