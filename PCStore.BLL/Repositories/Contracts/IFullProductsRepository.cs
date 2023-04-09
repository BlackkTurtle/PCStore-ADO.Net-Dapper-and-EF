using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PCStore.BLL.Entities;

namespace PCStore.BLL.Repositories.Contracts
{
   public interface IFullProductsRepository : IGenericRepository<FullProduct>
    {
        Task<FullProduct> GetFullProductsByArticleAsync(int Article);
    }
}
