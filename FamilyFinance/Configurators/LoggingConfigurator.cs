using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace FamilyFinance.Configurators;

/// <summary>
/// Конфигурация логирования
/// </summary>
internal static class LoggingConfigurator
{
    internal static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"))
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        
        return builder;
    }
}