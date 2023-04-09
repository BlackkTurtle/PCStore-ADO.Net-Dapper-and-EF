using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Forum.DAL.Repositories
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Users","UserID")
        {
        }
        public async Task<Users> GetUserByEmailAsync(string Email)
        {
            string sql = @"select * from Users where Email=@Email";
            var results = await _sqlConnection.QueryFirstOrDefaultAsync<Users>(sql, param: new { Email = Email },
                transaction: _dbTransaction);
            return results;
        }
        public async Task<Users> GetUserByPhoneAsync(string Phone)
        {
            string sql = @"select * from Users where Phone=@Phone";
            var results = await _sqlConnection.QueryFirstOrDefaultAsync<Users>(sql, param: new { Phone = Phone },
                transaction: _dbTransaction);
            return results;
        }
    }
}
