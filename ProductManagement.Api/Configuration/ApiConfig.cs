namespace ProductManagement.Api.Configuration;

public static class ApiConfig
{
    public static void AddpApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
    }

    public static void UsepApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();
    }
}
