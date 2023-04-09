using System;

namespace PC_Store.BLL.Repositories.Contracts
{
    public interface IBLLUnitOfWork : IDisposable
    {
        IFullProductsRepository _productsRepository { get; }
        void Commit();
        void Dispose();
    }
}
