using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts
{
    public interface IProductsRepository : IGenericRepository<Products>
    {
        Task<IEnumerable<Products>> GetProductsByNameLikeAsync(string LikeName);
        Task<IEnumerable<Products>> GetProductsByBrandAsync(int BrandID);
        Task<IEnumerable<Products>> GetProductsByTypeAsync(int TypeID);
    }
}
