using Microsoft.EntityFrameworkCore;
using PCStoreEF.DbContexts;
using PCStoreEF.EFRepositories.Contracts;
using PCStoreEF.Entities;
using PCStoreEF.Exceptions;

namespace PCStoreEF.EFRepositories;

public class EFPartOrdersRepository : EFGenericRepository<PartOrder>, IEFPartOrdersRepository
{
    public EFPartOrdersRepository(PCStoreDbContext databaseContext)
        : base(databaseContext)
    {
    }
    public async Task<IEnumerable<PartOrder>> GetAllPartOrdersByOrderIDAsync(int orderid)
    {
        return await databaseContext.PartOrders.Where(v => v.OrderId == orderid).ToListAsync()
            ?? throw new Exception($"Couldn't retrieve entities PartOrders");
    }
    public override async Task<PartOrder> GetCompleteEntityAsync(int id)
    {
        var my_event = await table.Include(ev => ev.Article)
                                 .Include(ev => ev.OrderId)
                                 .SingleOrDefaultAsync(ev => ev.PorderId == id);
        return my_event ?? throw new EntityNotFoundException("NOT FOUND");
    }
}
