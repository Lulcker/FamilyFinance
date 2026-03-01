using FamilyFinance.Core.Persistence;
using FamilyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFinance.Persistence.Configs;

internal sealed class IncomeConfig : EntityTypeConfigurationBase<Income>
{
    protected override void ConfigureMore(EntityTypeBuilder<Income> builder) { }
}