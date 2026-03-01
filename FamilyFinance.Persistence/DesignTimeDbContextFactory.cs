using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FamilyFinance.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FamilyFinanceDbContext>
{
    public FamilyFinanceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<FamilyFinanceDbContext>();

        builder.UseNpgsql("", _ => { });

        return new FamilyFinanceDbContext(builder.Options);
    }
}