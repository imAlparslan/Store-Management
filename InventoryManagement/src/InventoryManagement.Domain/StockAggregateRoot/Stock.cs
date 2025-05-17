using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
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
}