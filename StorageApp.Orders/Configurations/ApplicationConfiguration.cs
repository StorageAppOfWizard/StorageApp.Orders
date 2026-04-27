using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Application.Handlers;
using StorageApp.Orders.Application.Service;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Infrastructure.Authentication;
using StorageApp.Orders.Infrastructure.Repository;
using StorageProject.Application.Handlers;

namespace StorageApp.Orders.Web.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            services.AddScoped<IUserContextAuth, UserContextAuth>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMessagePublisher, MessagePublisher>();
            services.AddScoped<IOrderHandler, PendingHandler>();
            services.AddScoped<IOrderHandler, ApprovedHandler>();
            services.AddScoped<IOrderHandler, RejectHandler>();

        }
    }
}
