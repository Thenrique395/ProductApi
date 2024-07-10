using Microsoft.EntityFrameworkCore;
using ProductManagement.Api.Configuration;
using ProductManagement.Application.Configuration;
using ProductManagement.Infrastructure.Configuration;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Api;
internal class Program
{
    private Program()
    {
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSwagger();
        builder.Services.AddServices().AddRepository(builder.Configuration);
        builder.Services.AddMemoryCache();


        var app = builder.Build();

        app.UseSwaggerConfiguration();
        app.UsepApiConfiguration(app.Environment);

        app.UseHttpsRedirection();

        app.Run();
    }
}
