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

// Registra MessageConnection como Singleton e inicializa ela
builder.Services.AddScoped<IMessageConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var messageConnection = new MessageConnection();
    messageConnection.ConnectionMessage(config);
    messageConnection.InitializeAsync().GetAwaiter().GetResult(); // inicializa sync no startup
    return messageConnection;
});

builder.Services.AddScoped<IMessageProducer, MessageProducer>();
builder.Services.AddScoped<IMessageTopology, MessageTopology>();
builder.Services.AddScoped<IOrderService, OrderService>();


string connectionString = "User ID=root;Password=Lagavi30!;Host=localhost;Port=5432;Database=orders;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Lifetime=0;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddSwaggerGen();



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
