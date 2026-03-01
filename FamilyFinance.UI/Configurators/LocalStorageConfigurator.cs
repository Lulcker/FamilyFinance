using System.Text.Encodings.Web;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FamilyFinance.UI.Configurators;

/// <summary>
/// Конфигурация локального хранилища
/// </summary>
internal static class LocalStorageConfigurator
{
    internal static WebAssemblyHostBuilder ConfigureLocalStorage(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredLocalStorageAsSingleton(cfg =>
        {
            cfg.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            cfg.JsonSerializerOptions.WriteIndented = true;
        });
        
        return builder;
    }
}