using Dapper;
using PCStore.BLL.Entities;
using PCStore.BLL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PCStore.BLL.Repositories
{
    public class FullProductsRepository : GenericRepository<FullProduct>, IFullProductsRepository
    {
        public FullProductsRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Products", "Article")
        {
        }

        public async Task<FullProduct> GetFullProductsByArticleAsync(int BrandID)
        {
            string sql = @"select * from Products where BrandID=@BrandId";
            var results = await _sqlConnection.QuerySingleOrDefaultAsync<FullProduct>(sql, param: new { BrandId = BrandID },
                transaction: _dbTransaction);
            return results;
        }
    }
}
