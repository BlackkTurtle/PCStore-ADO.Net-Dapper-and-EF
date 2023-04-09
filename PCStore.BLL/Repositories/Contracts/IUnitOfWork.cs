using System;

namespace PCStore.BLL.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IFullProductsRepository _productsRepository { get; }
        void Commit();
        void Dispose();
    }
}
