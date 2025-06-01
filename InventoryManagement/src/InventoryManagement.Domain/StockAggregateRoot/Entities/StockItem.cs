using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.StockAggregateRoot.Entities;

public class StockItem : Entity<StockItemId>
{
    public ItemId ItemId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Capacity Capacity { get; private set; }

    public StockItem(ItemId itemId,
                     Quantity? quantity = null,
                     Capacity? capacity = null,
                     StockItemId? id = null) : base(id ?? StockItemId.CreateUnique())
    {
        ItemId = itemId;
        Quantity = quantity ??= new Quantity(0);
        Capacity = capacity ??= new Capacity(0);
    }
    private StockItem()
    {
        // EF Core requires a parameterless constructor for materialization
    }

    public void IncreaseQuantity(int amount)
    {
        Quantity.Increase(amount);
    }

    public void DecreaseQuantity(int amount)
    {
        Quantity.Decrease(amount);
    }

    public void SetQuantity(int quantity)
    {
        Quantity.SetQuantity(quantity);
    }

    public void SetCapacity(int capacity)
    {
        Capacity.SetCapacity(capacity);
    }

    public void IncreaseCapacity(int capacity)
    {
        Capacity.IncreaseCapacity(capacity);
    }
    public void DecreaseCapacity(int capacity)
    {
        Capacity.DecreaseCapacity(capacity);
    }

}

