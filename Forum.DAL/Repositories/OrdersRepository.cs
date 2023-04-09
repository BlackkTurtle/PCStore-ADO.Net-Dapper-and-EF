using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class OrdersRepository : GenericRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Orders","OrderID")
        {
        }
        public async Task<IEnumerable<Orders>> GetAllOrdersByUserIDAsync(int userid)
        {
            string sql = @"select * from Orders where UserID=@UserId";
            var results = await _sqlConnection.QueryAsync<Orders>(sql, param: new { UserId = userid },
                transaction: _dbTransaction);
            return results;
        }
    }
}
