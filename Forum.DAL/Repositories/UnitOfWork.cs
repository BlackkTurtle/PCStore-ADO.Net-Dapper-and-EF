using Forum.DAL.Repositories.Contracts;
using System;
using System.Data;

namespace Forum.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ITypeRepository _typeRepository { get; }
        public IBrandsRepository _brandRepository { get; }
        public ICommentsRepository _commentsRepository { get; }
        public IOrdersRepository _rdersRepository { get; }
        public IPartOrdersRepository _artOrdersRepository { get; }
        public IProductsRepository _productsRepository { get; }
        public IStatusesRepository _statusesRepository { get; }
        public IUsersRepository _usersRepository { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(
            ITypeRepository typeRepository,
            IBrandsRepository brandsRepository,
            ICommentsRepository commentsRepository,
            IOrdersRepository rdersRepository,
            IPartOrdersRepository artOrdersRepository,
            IProductsRepository productsRepository,
            IStatusesRepository statusesRepository,
            IUsersRepository usersRepository,
            IDbTransaction dbTransaction)
        {
            _typeRepository = typeRepository;
            _brandRepository = brandsRepository;
            _commentsRepository = commentsRepository;   
            _rdersRepository = rdersRepository;
            _artOrdersRepository = artOrdersRepository;
            _productsRepository = productsRepository;
            _statusesRepository = statusesRepository;
            _usersRepository = usersRepository;
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                //_dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}