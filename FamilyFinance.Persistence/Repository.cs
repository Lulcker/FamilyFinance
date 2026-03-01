using FamilyFinance.Core.Domain;
using FamilyFinance.Core.Persistence;


namespace FamilyFinance.Persistence;

public class Repository<TEntity> : Repository<FamilyFinanceDbContext, TEntity>, IRepository<TEntity> where TEntity : EntityBase
{
    public Repository(FamilyFinanceDbContext context) : base(context) { }
}

public interface IRepository<TEntity> : FamilyFinance.Core.Persistence.IRepository<TEntity> where TEntity : class;