using FamilyFinance.Application.Commands.Inits;
using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Infrastructure.Providers;

namespace FamilyFinance.Configurators;

internal static class DefaultUsersConfigurator
{
    internal static WebApplicationBuilder ConfigureDefaultUsers(this WebApplicationBuilder builder)
    {
        var defaultUsers = builder.Configuration.GetSection("DefaultUsers").Get<IReadOnlyCollection<DefaultUser>>();
        
        ArgumentNullException.ThrowIfNull(defaultUsers);
        
        builder.Services.AddSingleton<IDefaultUsersProvider>(_ => new DefaultUsersProvider
        {
            DefaultUsers = defaultUsers
        });
        
        return builder;
    }
    
    internal static async Task AddDefaultUsersAsync(this WebApplication builder) =>
        await builder.Services.CreateScope().ServiceProvider
            .GetRequiredService<CreateDefaultUsersCommand>()
            .ExecuteAsync();
}