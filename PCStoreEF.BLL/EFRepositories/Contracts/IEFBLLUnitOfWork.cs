namespace PCStoreEF.BLL.EFRepositories.Contracts;

public interface IEFBLLUnitOfWork
{
    IEFFullProductsRepository eFProductsRepository { get; }
}
