using Microsoft.EntityFrameworkCore;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;
using PCStoreEF.Exceptions;

namespace PCStoreEF.EFRepositories;

public class EFOrdersRepository : EFGenericRepository<Order>, IEFOrdersRepository
{
    public EFOrdersRepository(PCStoreDbContext databaseContext)
        : base(databaseContext)
    {
    }
    public async Task<IEnumerable<Order>> GetAllOrdersByUserIDAsync(int userid)
    {
        return await databaseContext.Orders.Where(v => v.UserId == userid).ToListAsync()
            ?? throw new Exception($"Couldn't retrieve entities Orders");
    }
    public override async Task<Order> GetCompleteEntityAsync(int id)
    {
        var my_event = await table.Include(ev => ev.UserId)
                                 .Include(ev => ev.StatusId)
                                 .SingleOrDefaultAsync(ev => ev.OrderId == id);
        return my_event ?? throw new EntityNotFoundException("NOT FOUND");
    }
}
