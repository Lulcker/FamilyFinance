using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FamilyFinance.UI.Configurators;

/// <summary>
/// Конфигурация HttpClient
/// </summary>
internal static class HttpClientConfigurator
{
    internal static WebAssemblyHostBuilder ConfigureHttpClient(this WebAssemblyHostBuilder builder)
    {
#if DEBUG
        const string apiUrl = "https://localhost:7151/";
#else
        var apiUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
        
        ArgumentException.ThrowIfNullOrWhiteSpace(apiUrl);
#endif
        
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiUrl) });
        
        return builder;
    }
}