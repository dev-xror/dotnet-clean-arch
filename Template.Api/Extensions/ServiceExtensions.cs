using Template.Contracts;
using Template.LoggerService;

namespace Template.Api.Extensions;

public static class ServiceExtensions
{
    // CORS (Cross-Origin Resource Sharing) is a mechanism to give or restrict
    // access rights to applications from different domains.
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        });

    // ASP.NET Core applications are by default self-hosted, and if we want to
    // host our application on IIS, we need to configure an IIS integration which
    // will eventually help us with the deployment to IIS
    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();


}