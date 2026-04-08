using StorageApp.Orders.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApplicationConfiguration();
builder.Services.AddInfrastructureConfiguration();
builder.Services.AddMessageBrokerConfiguration();
builder.Services.AddSwaggerConfiguration();




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
