using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories.Contracts;

public interface IEFOrdersRepository : IEFGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetAllOrdersByUserIDAsync(int userid);
}