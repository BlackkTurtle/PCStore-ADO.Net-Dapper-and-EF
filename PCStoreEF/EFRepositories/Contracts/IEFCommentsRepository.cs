using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories.Contracts;

public interface IEFCommentsRepository : IEFGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> GetAllCommentsByArticleAsync(int article);
}