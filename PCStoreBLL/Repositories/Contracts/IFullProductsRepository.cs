using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PCStoreBLL.Entities;

namespace PCStoreBLL.Repositories.Contracts
{
    public interface IFullProductsRepository : IGenericRepository<FullProduct>
    {
        Task<FullProduct> GetFullProductsByArticleAsync(int Article);
    }
}
