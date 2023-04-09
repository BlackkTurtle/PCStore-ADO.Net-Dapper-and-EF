

using System.Data.Entity;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;
using PCStoreEF.Exceptions;

namespace PCStoreEF.EFRepositories;

public class EFStatusesRepository : EFGenericRepository<Status>, IEFStatusesRepository
{
    public EFStatusesRepository(PCStoreDbContext databaseContext)
        : base(databaseContext)
    {
    }

    public override async Task<Status> GetCompleteEntityAsync(int id)
    {
        throw new NotImplementedException();
    }
}
