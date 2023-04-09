using PCStoreEF.BLL.EFRepositories.Contracts;
using PCStoreEF.BLL.Entities;
using PCStoreEF.EFRepositories;
using PCStoreEF.EFRepositories.Contracts;

namespace PCStoreEF.BLL.EFRepositories;

public class EFFullProductsRepository : IEFFullProductsRepository
{
    public async Task<FullProduct> GetCompleteFullProductAsync(IEFUnitOfWork ef_uow, int article)
    {
        var product = await ef_uow.eFProductsRepository.GetByIdAsync(article);
        var brand = await ef_uow.eFBrandsRepository.GetByIdAsync(product.BrandId);
        var type = await ef_uow.EFTypesRepository.GetByIdAsync(product.Type);
        var fullproduct = new FullProduct
        {
            Article=product.Article,
            Name=product.Name,
            Picture=product.Picture,
            Type=product.Type,
            Price=product.Price,
            ProductInfo=product.ProductInfo,
            BrandId=product.BrandId,
            Availability=product.Availability,
            BrandName=brand.BrandName,
            TypeName=type.TypeName
        };
        if (product == null)
        {
            throw new ArgumentException("NOT FOUND");
        }
        else
        {
            return fullproduct;
        }
    }
}
