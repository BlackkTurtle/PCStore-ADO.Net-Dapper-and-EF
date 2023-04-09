using System;

namespace PCStoreBLL.Repositories.Contracts
{
    public interface IBLLUnitOfWork : IDisposable
    {
        IFullProductsRepository _productsRepository { get; }
        void Commit();
        void Dispose();
    }
}
