using FamilyFinance.Core.Persistence;

namespace FamilyFinance.Persistence;

public class UnitOfWork : UnitOfWork<FamilyFinanceDbContext>, IUnitOfWork
{
    public UnitOfWork(FamilyFinanceDbContext context) : base(context) { }
}

public interface IUnitOfWork : FamilyFinance.Core.Persistence.IUnitOfWork;