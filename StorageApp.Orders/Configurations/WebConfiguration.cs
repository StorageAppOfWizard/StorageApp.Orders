using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace StorageApp.Orders.Web.Configurations
{
    public static class WebConfiguration
    {
        public static void AddWebConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var key = config["Jwt:Key"] ?? "default-key";
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(key)),
                        NameClaimType = "unique_name",

                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
                options.AddPolicy("AdminOrManager", policy => policy.RequireRole("Manager", "Admin"));
            });

            //services.AddOpenTelemetry()
            //    .ConfigureResource(r => r.AddService("ProductApi"))
            //    .WithTracing(t =>
            //    {
            //        t.AddAspNetCoreInstrumentation()
            //        .AddOtlpExporter();
            //    })
            //    .WithLogging(l => l.AddOtlpExporter())
            //    .WithMetrics(m => m.AddMeter());

            services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));



        }
    }
}
