using Microsoft.EntityFrameworkCore;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Exceptions;

namespace PCStoreEF.EFRepositories;

public abstract class EFGenericRepository<TEntity> : IEFGenericRepository<TEntity> where TEntity : class
{
    // properties
    protected readonly PCStoreDbContext databaseContext;
    protected readonly DbSet<TEntity> table;

    // constructor
    public EFGenericRepository(PCStoreDbContext databaseContext)
    {
        this.databaseContext = databaseContext;
        table = this.databaseContext.Set<TEntity>();
    }


    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns>TEntity</returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        return await table.FindAsync(id)
        ?? throw new EntityNotFoundException($"{typeof(TEntity).Name} with id {id} not found.");
    }


    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns>IEnumerable<TEntity></returns>
    /// <exception cref="Exception"></exception>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await table.ToListAsync()
        ?? throw new Exception($"Couldn't retrieve entities {typeof(TEntity).Name} ");
    }


    /// <summary>
    /// AddAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual async Task AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(TEntity)} entity must not be null");
        }
        await table.AddAsync(entity);
    }


    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(TEntity)} entity must not be null");
        }
        await Task.Run(() => table.Update(entity));

    }

    /// <summary>
    /// DeleteByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id) ?? throw new EntityNotFoundException($"{typeof(TEntity).Name} with id {id} not found. Cann't delete.");
        await Task.Run(() => table.Remove(entity));
    }


    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }
        await Task.Run(() => table.Remove(entity));
    }

    /// <summary>
    /// GetCompleteEntityAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract Task<TEntity> GetCompleteEntityAsync(int id);
}
