

using System.Data.Entity;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories;

public class EFBrandsRepository : EFGenericRepository<Brand>, IEFBrandsRepository
{
    public EFBrandsRepository(PCStoreDbContext databaseContext)
        : base(databaseContext)
    {
    }

    public override Task<Brand> GetCompleteEntityAsync(int id)
    {
        throw new NotImplementedException();
    }
}
