using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Services.Interfaces;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Data.Repositories;
using ProductManagement.Infrastructure.Kafka;

namespace ProductManagement.Infrastructure.Configuration;

public static class InfrastructureModule
{

    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IKafkaProducer, KafkaProducer>();

        // Register repositories
        services.AddScoped<IRepository, ProductRepository>();

        // Adicionar o KafkaProducer

        return services.ConfigureKafka(configuration).ConfigureSqlServer(configuration);
    }

    public static IServiceCollection ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Kafka configuration
        var kafkaConfig = configuration.GetSection("Kafka");
        services.AddSingleton(sp => kafkaConfig["BootstrapServers"]);
        services.AddSingleton(sp => kafkaConfig["Topic"]);
        return services.AddSingleton<IKafkaProducer>(sp => new KafkaProducer("localhost:9092")); // Altere para o seu bootstrap server do Kafka
    }

    public static IServiceCollection ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
    {

        return services.AddDbContext<ProductDbContext>(options =>
         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
         {
             sqlOptions.EnableRetryOnFailure(
                 maxRetryCount: 5,
                 maxRetryDelay: TimeSpan.FromSeconds(30),
                 errorNumbersToAdd: null);
         }));
    }
}

