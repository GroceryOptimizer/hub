using Api.Services;

namespace Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
