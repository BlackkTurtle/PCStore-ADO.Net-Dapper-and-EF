

using System.Data.Entity;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories;

public class EFTypesRepository : EFGenericRepository<Types>, IEFTypesRepository
{
    public EFTypesRepository(PCStoreDbContext databaseContext)
        : base(databaseContext)
    {
    }
    public override Task<Types> GetCompleteEntityAsync(int id)
    {
        throw new NotImplementedException();
    }
}
