using Microsoft.EntityFrameworkCore;
using StorageApp.Orders.Domain.Entity;


namespace StorageApp.Orders.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(
                entity => entity
                .Property(e => e.Status)
                .HasConversion<string>());
        }
    }
}
