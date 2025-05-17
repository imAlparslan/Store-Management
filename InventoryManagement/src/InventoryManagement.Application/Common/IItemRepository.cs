using InventoryManagement.Domain.ItemAggregateRoot;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;

namespace InventoryManagement.Application.Common;
public interface IItemRepository
{
    Task<Item> InsertItemAsync(Item item, CancellationToken cancellationToken = default);
    Task<IEnumerable<Item>> GetAllItemsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Item>> GetAllDefaultStockItems(CancellationToken cancellationToken = default);
    Task<Item?> GetItemById(ItemId itemId, CancellationToken cancellationToken = default);
    Task<Item> UpdateItemAsync(Item item, CancellationToken cancellationToken = default);
}

