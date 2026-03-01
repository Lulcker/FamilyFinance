using System.Text;
using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Application.Contracts.Services;
using FamilyFinance.Infrastructure.Providers;
using FamilyFinance.Infrastructure.Services;

namespace FamilyFinance.Configurators;

internal static class HashConfigurator
{
    internal static WebApplicationBuilder ConfigureHash(this WebApplicationBuilder builder)
    {
        var pepper = builder.Configuration.GetValue<string>("Hash:Pepper");
        
        ArgumentException.ThrowIfNullOrWhiteSpace(pepper);
        
        builder.Services.AddSingleton<IHashProvider>(_ => new HashProvider
        {
            PepperBytes = Encoding.UTF8.GetBytes(pepper)
        });

        builder.Services.AddSingleton<IHashService, HashService>();
        
        return builder;
    }
}