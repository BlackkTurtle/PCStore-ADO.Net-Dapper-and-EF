using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts
{
    public interface ITypeRepository : IGenericRepository<Types>
    {
        Task<IEnumerable<Types>> TopFiveTypesAsync();
    }
}
