using Core;
using Data;
using Microsoft.EntityFrameworkCore;
using StoreApi;
using StoreApi.Services;

namespace Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Services
        // Database
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAllLocalhost",
                policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:3000",
                            "http://localhost:5173",
                            "http://localhost:8080",
                            "http://localhost:5000",
                            "http://localhost:7049"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            );
        });

        // Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        // Register StoreClient Service
        builder.Services.AddScoped<StoreClient>();

        // Controllers
        builder.Services.AddControllers();

        // Register GRPC Service
        builder.Services.AddGrpc();

        var app = builder.Build();

        // Ensure database is deleted and migrated before starting the application
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        }

        // Always use swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        // Listen on GRPC handshake
        app.UseCors("AllowAllLocalhost");

        app.MapGrpcService<HubServer>();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
