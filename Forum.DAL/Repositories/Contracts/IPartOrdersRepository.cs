using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts
{
    public interface IPartOrdersRepository : IGenericRepository<PartOrders>
    {
        Task<IEnumerable<PartOrders>> GetAllPartOrdersByOrderIDAsync(int orderid);
    }
}
