using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts
{
    public interface ICommentsRepository : IGenericRepository<Comments>
    {
        Task<IEnumerable<Comments>> GetAllCommentsByArticleAsync(int article);
    }
}
