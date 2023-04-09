using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts
{
    public interface IOrdersRepository : IGenericRepository<Orders>
    {
        Task<IEnumerable<Orders>> GetAllOrdersByUserIDAsync(int userid);
    }
}
