using System.Data;
using PCStoreEF.BLL.EFRepositories.Contracts;

namespace PCStoreEF.BLL.EFRepositories;

public class EFBLLUnitOfWork : IEFBLLUnitOfWork
{
    public IEFFullProductsRepository eFProductsRepository { get; }


    public EFBLLUnitOfWork(
        IEFFullProductsRepository eFProductsRepository)
    {
        this.eFProductsRepository = eFProductsRepository;
    }
}