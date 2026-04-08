using Microsoft.EntityFrameworkCore;
using StorageApp.Orders.Infrastructure;

namespace StorageApp.Orders.Web.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services)
        {
            string connectionString = "User ID=root;Password=Lagavi30!;Host=localhost;Port=5432;Database=orders;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;";
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
