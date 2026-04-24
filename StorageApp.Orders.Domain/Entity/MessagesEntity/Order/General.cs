namespace StorageApp.Orders.Domain.Entity.MessagesEntity.Order
{
    public class General
    {
        public Guid Id { get; set; }
        public DateTime EventAt { get; set; } = DateTime.Now;
    }
}
