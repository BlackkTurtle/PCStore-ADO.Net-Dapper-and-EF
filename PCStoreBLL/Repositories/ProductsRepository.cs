using Dapper;
using PCStoreBLL.Entities;
using PCStoreBLL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PCStoreBLL.Repositories
{
    public class FullProductsRepository : GenericRepository<FullProduct>, IFullProductsRepository
    {
        public FullProductsRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Products", "Article")
        {
        }

        public async Task<FullProduct> GetFullProductsByArticleAsync(int Article)
        {
            string sql = @"select Article,t1.Name,Picture,t1.Type,Price,ProductInfo,t1.BrandID,t1.Availability,t2.BrandName,t3.TypeName from Products as t1,Brands as t2,Types as t3 where Article=@Article and t1.BrandID=t2.BrandID and t1.Type=t3.Id";
            var results = await _sqlConnection.QuerySingleOrDefaultAsync<FullProduct>(sql, param: new { Article },
                transaction: _dbTransaction);
            return results;
        }
    }
}
