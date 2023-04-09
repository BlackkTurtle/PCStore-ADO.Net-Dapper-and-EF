using System;
using System.Data;
using PC_Store.BLL.Repositories.Contracts;

namespace PC_Store.BLL.Repositories
{
    public class BLLUnitOfWork : IBLLUnitOfWork, IDisposable
    {
        public IFullProductsRepository _productsRepository { get; }
        readonly IDbTransaction _dbTransaction;

        public BLLUnitOfWork(
            IFullProductsRepository productsRepository,
            IDbTransaction dbTransaction)
        {
            _productsRepository = productsRepository;
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