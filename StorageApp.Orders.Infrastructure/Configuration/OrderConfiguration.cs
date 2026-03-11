using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.QuantityProduct).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            //builder.Property(x => x.UserId).IsRequired();
            //builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
