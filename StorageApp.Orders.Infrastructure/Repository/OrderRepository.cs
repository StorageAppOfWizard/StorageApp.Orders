using Microsoft.EntityFrameworkCore;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Infrastructure.Repository
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<Order> _dbSet = context.Set<Order>();


        public async Task Create(Order order)
            => await _dbSet.AddAsync(order);
        
        public async Task<IEnumerable<Order>> GetAll(Order order, CancellationToken cancellationToken)
            => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        
        public async Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default)
            => await _dbSet .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public void Delete(Order order, CancellationToken cancellationToken = default)
            =>_dbSet.Remove(order);
        
        //public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId, CancellationToken cancellationToken = default)
        //{
        //    return await _context.Orders
        //        .Where(o => o.UserId == userId)
        //        .AsNoTracking()
        //        .ToListAsync(cancellationToken);
        //}

        public async Task<IEnumerable<Order>> GetOrderWithIncludes(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order?>> GetPagedAsync(int page, int pageQuantity, CancellationToken cancellationToken = default)
        {

            return await _dbSet
                .Skip((page - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync(cancellationToken);
            
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<ProductSnap?> GetProductById(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

