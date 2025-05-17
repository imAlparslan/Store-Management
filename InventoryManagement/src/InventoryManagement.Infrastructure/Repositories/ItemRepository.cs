using InventoryManagement.Application.Common;
using InventoryManagement.Domain.ItemAggregateRoot;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;
public class ItemRepository(InventoryDbContext context, IUnitOfWorkManager unitOfWorkManager) : IItemRepository
{
    private readonly InventoryDbContext _dbContext = context;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

    public async Task<IEnumerable<Item>> GetAllDefaultStockItems(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Items.Where(x => x.IsDefaultStockItem).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Items.ToListAsync(cancellationToken);
    }

    public async Task<Item?> GetItemById(ItemId itemId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Items.FindAsync(itemId, cancellationToken);
    }

    public async Task<Item> InsertItemAsync(Item item, CancellationToken cancellationToken = default)
    {
        await _dbContext.Items.AddAsync(item, cancellationToken);
        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        return item;
    }

    public async Task<Item> UpdateItemAsync(Item item, CancellationToken cancellationToken = default)
    {
        _dbContext.Items.Update(item);
        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        return item;
    }
}

