using FamilyFinance.Persistence.Configs;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Persistence;

public class FamilyFinanceDbContext : DbContext
{
    public FamilyFinanceDbContext(DbContextOptions<FamilyFinanceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
    }
}