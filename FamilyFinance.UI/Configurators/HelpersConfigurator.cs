using FamilyFinance.ProxyApiMethods;
using FamilyFinance.UI.Contracts;
using FamilyFinance.UI.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FamilyFinance.UI.Configurators;

/// <summary>
/// Конфигурация помощников
/// </summary>
internal static class HelpersConfigurator
{
    internal static WebAssemblyHostBuilder ConfigureHelpers(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IHttpHelper, HttpHelper>();

        builder.Services.AddSingleton<ISnackbarHelper, SnackbarHelper>();
        
        builder.Services.AddSingleton<IBreadcrumbHelper, BreadcrumbHelper>();

        builder.Services.AddApiHelpers();

        return builder;
    }
}