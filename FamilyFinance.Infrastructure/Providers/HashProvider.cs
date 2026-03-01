using FamilyFinance.Application.Contracts.Providers;

namespace FamilyFinance.Infrastructure.Providers;

public class HashProvider : IHashProvider
{
    public required byte[] PepperBytes { get; init; }
}