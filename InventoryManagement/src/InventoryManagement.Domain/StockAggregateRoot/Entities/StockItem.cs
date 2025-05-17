using Ardalis.GuardClauses;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.StockAggregateRoot.Entities;
public class StockItem : Entity<StockItemId>
{
    public ItemId ItemId { get; private set; }
    public int Quantatiy { get; private set; }
    public int Capacity { get; private set; }

    public StockItem(ItemId itemId,
                     int quantatiy = 0,
                     int capacity = 0,
                     StockItemId? id = null) : base(id ?? StockItemId.CreateUnique())
    {
        ItemId = itemId;
        Quantatiy = Guard.Against.Negative(quantatiy);
        Capacity = Guard.Against.Negative(capacity);
    }
}

