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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Ensure database is deleted and migrated before starting the application
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        }

        // Listen on GRPC handshake
        app.MapGrpcService<HubServer>();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
