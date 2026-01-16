using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Domain.StockAggregateRoot.Errors;
using InventoryManagement.Domain.StockAggregateRoot.Exceptions;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
namespace InventoryManagement.Domain.StockAggregateRoot;

public sealed class Stock : AggregateRoot<StockId>
{
    public StoreId StoreId { get; init; }
    private readonly List<StockItem> _stockItems = new();
    public IReadOnlyList<StockItem> StockItems => _stockItems;
    private readonly List<Guid> _groupIds = new();
    public IReadOnlyList<Guid> GroupIds => _groupIds;
    public Stock(StoreId storeId, List<Guid> GroupIds, List<StockItem> items, StockId? id = null)
        : base(id ?? StockId.CreateUnique())
    {
        StoreId = storeId;
        _groupIds = GroupIds;
        _stockItems = items;
    }
    private Stock()
    {
    }

    public bool HasStockItem(StockItemId stockItemId)
        => _stockItems.Any(x => x.Id == stockItemId);
    public void IncreaseStockItemCapacity(StockItemId stockItemId, int amount)
    {
        var item = _stockItems.FirstOrDefault(x => x.Id == stockItemId);

        if (item is null)
        {
            throw StockException.Create(StockErrors.StockItemNotFound);
        }

        item.IncreaseCapacity(amount);
    }
    public bool TryAddGroup(Guid groupId)
    {
        if (_groupIds.Contains(groupId))
            return false;

        _groupIds.Add(groupId);
        return true;
    }
    public bool TryAddItem(StockItem stockItem)
    {
        if (_stockItems.Any(x => x.ItemId == stockItem.ItemId))
            return false;

        _stockItems.Add(stockItem);

        return true;
    }
}