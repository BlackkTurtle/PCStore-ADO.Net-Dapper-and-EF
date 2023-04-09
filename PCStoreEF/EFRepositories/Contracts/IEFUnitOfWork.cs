namespace PCStoreEF.EFRepositories.Contracts;

public interface IEFUnitOfWork
{
    IEFTypesRepository EFTypesRepository { get; }
    IEFBrandsRepository eFBrandsRepository { get; }
    IEFCommentsRepository eFCommentsRepository { get; }
    IEFOrdersRepository eFOrdersRepository { get; }
    IEFPartOrdersRepository eFPartOrdersRepository { get; }
    IEFProductsRepository eFProductsRepository { get; }
    IEFStatusesRepository eFStatusesRepository { get; }
    IEFUsersRepository eFUsersRepository { get; }
    Task SaveChangesAsync();
}
