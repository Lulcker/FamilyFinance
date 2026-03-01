using System.Text;
using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Application.Contracts.Services;
using FamilyFinance.Infrastructure.Providers;
using FamilyFinance.Infrastructure.Services;

namespace FamilyFinance.Configurators;

internal static class AesCryptoConfigurator
{
    internal static WebApplicationBuilder ConfigureAesCrypto(this WebApplicationBuilder builder)
    {
        var key = builder.Configuration.GetValue<string>("Aes:Key");
        
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        
        var iv = builder.Configuration.GetValue<string>("Aes:IV");
        
        ArgumentException.ThrowIfNullOrWhiteSpace(iv);
        
        builder.Services.AddSingleton<IAesCryptoProvider>(_ => new AesCryptoProvider
        {
            Key = Encoding.UTF8.GetBytes(key),
            IV = Encoding.UTF8.GetBytes(iv)
        });

        builder.Services.AddSingleton<IAesCryptoService, AesCryptoService>();
        
        return builder;
    }
}