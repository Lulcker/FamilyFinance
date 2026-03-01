using FamilyFinance.Core.Persistence;
using FamilyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFinance.Persistence.Configs;

internal sealed class CategoryConfig : EntityTypeConfigurationBase<Category>
{
    protected override void ConfigureMore(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(u => u.Expenses)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}