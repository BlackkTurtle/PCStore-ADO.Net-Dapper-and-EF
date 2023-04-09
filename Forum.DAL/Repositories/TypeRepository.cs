using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class TypeRepository : GenericRepository<Types>, ITypeRepository
    {
        public TypeRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Types","Id")
        {
        }
        public async Task<IEnumerable<Types>> TopFiveTypesAsync()
        {
            string sql = @"SELECT TOP 5 * FROM Types";
            var results = await _sqlConnection.QueryAsync<Types>(sql,
                transaction: _dbTransaction);
            return results;
        }
    }
}
