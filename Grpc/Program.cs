using Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to allow HTTP/2 on HTTP endpoints.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5241, listenOptions =>
    {
        // Force HTTP/2 without TLS (not recommended for production)
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<HubServer>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();