using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories.Contracts;

public interface IEFProductsRepository : IEFGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByNameLikeAsync(string LikeName);
    Task<IEnumerable<Product>> GetProductsByBrandAsync(int BrandID);
    Task<IEnumerable<Product>> GetProductsByTypeAsync(int TypeID);
}