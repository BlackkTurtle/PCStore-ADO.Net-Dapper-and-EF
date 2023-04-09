using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Products","Article")
        {
        }

        public async Task<IEnumerable<Products>> GetProductsByBrandAsync(int BrandID)
        {
            string sql = @"select * from Products where BrandID=@BrandId";
            var results = await _sqlConnection.QueryAsync<Products>(sql, param: new {BrandId=BrandID},
                transaction: _dbTransaction);
            return results;
        }

        public async Task<IEnumerable<Products>> GetProductsByNameLikeAsync(string LikeName)
        {
            string sql = @"select * from Products where Name Like '%@likeName%'";
            var results = await _sqlConnection.QueryAsync<Products>(sql, param: new { likeName = LikeName },
                transaction: _dbTransaction);
            return results;
        }

        public async Task<IEnumerable<Products>> GetProductsByTypeAsync(int TypeID)
        {
            string sql = @"select * from Products where Type=@TypeId";
            var results = await _sqlConnection.QueryAsync<Products>(sql, param: new { TypeId = TypeID },
                transaction: _dbTransaction);
            return results;
        }
    }
}
