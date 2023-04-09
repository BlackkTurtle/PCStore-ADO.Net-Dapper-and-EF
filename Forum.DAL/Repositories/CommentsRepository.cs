using Dapper;
using Forum.DAL.Entities;
using Forum.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Forum.DAL.Repositories
{
    public class CommentsRepository : GenericRepository<Comments>, ICommentsRepository
    {
        public CommentsRepository(SqlConnection sqlConnection, IDbTransaction dbtransaction) : base(sqlConnection, dbtransaction, "Comments","CommentID")
        {
        }
        public async Task<IEnumerable<Comments>> GetAllCommentsByArticleAsync(int article)
        {
            string sql = @"SELECT * FROM Comments where Article=@Article";
            var results = await _sqlConnection.QueryAsync<Comments>(sql, param: new { Article = article },
                transaction: _dbTransaction);
            return results;
        }
    }
}
