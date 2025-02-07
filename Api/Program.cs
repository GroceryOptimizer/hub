using Api.Services;

using Core;

using Data;

using Grpc.Services;

using HubClient;

using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Kestrel to allow HTTP/2 on HTTP endpoints.
            builder.WebHost.ConfigureKestrel(options =>
            {
                //Port for HTTP 1.x requests
                options.ListenAnyIP(7049, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
                });
                //Port for HTTP/2 requests
                options.ListenAnyIP(5241, listenoptions =>
                {
                    listenoptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                });
            });


            // Services
            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Controllers
            builder.Services.AddControllers();

            // Register Connector Service
            builder.Services.AddScoped<IConnectorService, ConnectorService>();

            // Register GRPC Service
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.MapGrpcService<HubServer>();

            // Seed data if empty db
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!await context.Vendors.AnyAsync() || !await context.Coordinates.AnyAsync())
                {
                    await SeedData.InitAsync(context);
                }
            }

            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
