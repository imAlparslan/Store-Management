using InventoryManagement.Domain.Models;

namespace InventoryManagement.Domain.StockAggregateRoot;
public sealed class Stock : AggregateRoot
{
    public Guid StoreId { get; init; }
    private readonly List<Guid> _groupIds = new();
    public IReadOnlyList<Guid> GroupIds => _groupIds;
    public Stock(Guid storeId,List<Guid> GroupIds, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        StoreId = storeId;
        _groupIds = GroupIds;
    }
}