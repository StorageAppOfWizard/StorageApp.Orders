namespace StorageApp.Orders.Application.DTO
{
    public record CreateOrderDTO
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; set; }
    }
}
