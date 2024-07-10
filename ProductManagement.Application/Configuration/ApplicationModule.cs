using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Services;
using ProductManagement.Application.Services.Interfaces;

namespace ProductManagement.Application.Configuration;

public static class ApplicationModule
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Register application services
        services.AddScoped<IProductService, ProductService>();

        return services.ConfigureAutoMapper();
    }

    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(ApplicationModule).Assembly);

    }
}
