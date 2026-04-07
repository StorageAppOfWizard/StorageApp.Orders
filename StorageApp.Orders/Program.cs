using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StorageApp.Orders.Application;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Application.Service;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Domain.Entity;
using StorageApp.Orders.Infrastructure;
using StorageApp.Orders.Infrastructure.Repository;
using StorageApp.Orders.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMessagePublisher, MessagePublisher>();

string connectionString = "User ID=root;Password=Lagavi30!;Host=localhost;Port=5432;Database=orders;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(busConfig =>
{
    busConfig.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.Message<OrderMessage>(m =>
        {
            m.SetEntityName("order-created");

        });

        config.Publish<OrderMessage>(m =>
        {
            m.ExchangeType = ExchangeType.Fanout;
        });

        config.ConfigureEndpoints(context);
        
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
