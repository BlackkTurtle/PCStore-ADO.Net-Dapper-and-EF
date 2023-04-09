using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories.Contracts;

public interface IEFPartOrdersRepository : IEFGenericRepository<PartOrder>
{
    Task<IEnumerable<PartOrder>> GetAllPartOrdersByOrderIDAsync(int orderid);
}