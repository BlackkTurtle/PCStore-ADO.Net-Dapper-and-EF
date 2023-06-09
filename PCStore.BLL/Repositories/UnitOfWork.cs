﻿using PCStore.BLL.Repositories.Contracts;
using System;
using System.Data;

namespace PCStore.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IFullProductsRepository _productsRepository { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(
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