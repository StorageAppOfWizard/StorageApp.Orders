namespace StorageApp.Orders.Domain.Entity
{
    public class ProductSnap
    {
        public Guid Id { get; set; }
        public int Quantity { get; set ; }     
        public bool IsActive { get; set; } = true;
    }
}
