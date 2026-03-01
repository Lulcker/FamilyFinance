using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Infrastructure.Providers;

namespace FamilyFinance.Configurators;

/// <summary>
/// Конфигурация CORS
/// </summary>
internal static class CorsConfigurator
{
    internal static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
    {
        
#if DEBUG
        const string webUrl = "https://localhost:7009";
#else
        var webUrl = builder.Configuration.GetValue<string>("WebBaseUrl");
        
        ArgumentException.ThrowIfNullOrWhiteSpace(webUrl);
#endif
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy => 
                policy.WithOrigins(webUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        builder.Services.AddSingleton<IUrlUIProvider>(new UrlUIProvider
        {
            Url = webUrl
        });

        return builder;
    }
}