using FamilyFinance.Application.Contracts.Providers;

namespace FamilyFinance.Infrastructure.Providers;

public class AesCryptoProvider : IAesCryptoProvider
{
    public required byte[] Key { get; init; }
    
    public required byte[] IV { get; init; }
}