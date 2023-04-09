

using PCStoreEF.BLL.Entities;
using PCStoreEF.EFRepositories.Contracts;

namespace PCStoreEF.BLL.EFRepositories.Contracts;

public interface IEFFullProductsRepository
{
    public Task<FullProduct> GetCompleteFullProductAsync(IEFUnitOfWork ef_uow,int article);
}