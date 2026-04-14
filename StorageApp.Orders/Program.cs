using StorageApp.Orders.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddWebConfiguration(builder.Configuration);
builder.Services.AddMessageBrokerConfiguration();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
