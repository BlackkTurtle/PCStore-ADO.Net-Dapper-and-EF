using System;

namespace Forum.DAL.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ITypeRepository _typeRepository { get; }
        IBrandsRepository _brandRepository { get; }
        ICommentsRepository _commentsRepository { get; }
        IOrdersRepository _rdersRepository { get; }
        IPartOrdersRepository _artOrdersRepository { get; }
        IProductsRepository _productsRepository { get; }
        IStatusesRepository _statusesRepository { get; }
        IUsersRepository _usersRepository { get; }
        void Commit();
        void Dispose();
    }
}
