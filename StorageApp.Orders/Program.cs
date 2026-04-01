using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StorageApp.Orders.Application;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Application.Service;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Infrastructure;
using StorageApp.Orders.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

string connectionString = "User ID=root;Password=Lagavi30!;Host=localhost;Port=5432;Database=orders;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(busConfig =>
{
    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
        config.Host
        (
            host: "localhost",
            virtualHost: "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                }
        );
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
