using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PC_Store.BLL.Entities;

namespace PC_Store.BLL.Repositories.Contracts
{
    public interface IFullProductsRepository : IGenericRepository<FullProduct>
    {
        Task<FullProduct> GetProductsByBrandAsync(int Article);
    }
}
