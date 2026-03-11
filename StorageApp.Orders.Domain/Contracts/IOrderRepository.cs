using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Domain.Contracts
{
    public interface IOrderRepository
    {
        //public Task<IEnumerable<Order>> GetOrdersByUserId(string userId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order>> GetOrderWithIncludes(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order>> GetAll(Order order, CancellationToken cancellationToken);
        public Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default);
        public Task<ProductSnap?> GetProductById(Guid id, CancellationToken cancellationToken = default);
        public void Delete(Order order, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order?>> GetPagedAsync(int page, int pageQuantity, CancellationToken cancellationToken = default);
        public Task Create(Order order);
        public Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
