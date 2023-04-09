using System.Data;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;

namespace PCStoreEF.EFRepositories;

public class EFUnitOfWork : IEFUnitOfWork
{
    protected readonly PCStoreDbContext databaseContext;

    public IEFBrandsRepository eFBrandsRepository { get; }

    public IEFCommentsRepository eFCommentsRepository { get; }

    public IEFOrdersRepository eFOrdersRepository { get; }

    public IEFPartOrdersRepository eFPartOrdersRepository { get; }

    public IEFProductsRepository eFProductsRepository { get; }

    public IEFStatusesRepository eFStatusesRepository { get; }

    public IEFUsersRepository eFUsersRepository { get; }

    public IEFTypesRepository EFTypesRepository { get; }

    public EFUnitOfWork(
        PCStoreDbContext databaseContext,
        IEFBrandsRepository eFBrandsRepository,
        IEFCommentsRepository eFCommentsRepository,
        IEFOrdersRepository eFOrdersRepository,
        IEFPartOrdersRepository eFPartOrdersRepository,
        IEFProductsRepository eFProductsRepository,
        IEFStatusesRepository eFStatusesRepository,
        IEFTypesRepository eFTypesRepository,
        IEFUsersRepository eFUsersRepository)
    {
        this.databaseContext = databaseContext;
        this.EFTypesRepository = eFTypesRepository;
        this.eFBrandsRepository=eFBrandsRepository;
        this.eFProductsRepository=eFProductsRepository;
        this.eFPartOrdersRepository=eFPartOrdersRepository;
        this.eFCommentsRepository=eFCommentsRepository;
        this.eFOrdersRepository=eFOrdersRepository;
        this.eFUsersRepository=eFUsersRepository;
        this.eFStatusesRepository = eFStatusesRepository;
    }

    public async Task SaveChangesAsync()
    {
        await databaseContext.SaveChangesAsync();
    }
}