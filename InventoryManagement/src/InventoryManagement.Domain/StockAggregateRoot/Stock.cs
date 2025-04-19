using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
namespace InventoryManagement.Domain.StockAggregateRoot;
public sealed class Stock : AggregateRoot<StockId>
{
    public Guid StoreId { get; init; }

    private readonly List<Guid> _groupIds = new();
    public IReadOnlyList<Guid> GroupIds => _groupIds;
    public Stock(Guid storeId, List<Guid> GroupIds, StockId? id = null)
        : base(id ?? StockId.CreateUnique())
    {
        StoreId = storeId;
        _groupIds = GroupIds;
    }
}