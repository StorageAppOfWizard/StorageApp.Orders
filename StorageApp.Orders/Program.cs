using StorageApp.Orders.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebConfiguration(builder.Configuration);
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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
