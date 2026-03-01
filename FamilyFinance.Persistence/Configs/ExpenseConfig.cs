using FamilyFinance.Core.Persistence;
using FamilyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFinance.Persistence.Configs;

internal sealed class ExpenseConfig : EntityTypeConfigurationBase<Expense>
{
    protected override void ConfigureMore(EntityTypeBuilder<Expense> builder) { }
}