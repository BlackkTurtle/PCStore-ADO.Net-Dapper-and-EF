using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class PartOrdersRepository : GenericRepository<PartOrders>, IPartOrdersRepository
    {
        public PartOrdersRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "PartOrders","POrderID")
        {
        }
        public async Task<IEnumerable<PartOrders>> GetAllPartOrdersByOrderIDAsync(int orderid)
        {
            string sql = @"select * from PartOrders where OrderID=@OrderId";
            var results = await _sqlConnection.QueryAsync<PartOrders>(sql, param: new { OrderId = orderid },
                transaction: _dbTransaction);
            return results;
        }
    }
}
