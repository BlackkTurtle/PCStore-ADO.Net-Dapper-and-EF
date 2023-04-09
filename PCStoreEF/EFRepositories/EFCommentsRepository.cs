using Microsoft.EntityFrameworkCore;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;
using PCStoreEF.Exceptions;

namespace PCStoreEF.EFRepositories;

public class EFCommentRepository : EFGenericRepository<Comment>, IEFCommentsRepository
{
    public EFCommentRepository(PCStoreDbContext databaseContext)
        : base(databaseContext)
    {
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsByArticleAsync(int article)
    {
        return await databaseContext.Comments.Where(v => v.Article == article).ToListAsync()
            ?? throw new Exception($"Couldn't retrieve entities Comment");
    }

    public override async Task<Comment> GetCompleteEntityAsync(int id)
    {
        var my_event = await table.Include(ev => ev.UserId)
                                 .Include(ev => ev.Article)
                                 .SingleOrDefaultAsync(ev => ev.CommentId == id);
        return my_event ?? throw new EntityNotFoundException("NOT FOUND");
    }
}


