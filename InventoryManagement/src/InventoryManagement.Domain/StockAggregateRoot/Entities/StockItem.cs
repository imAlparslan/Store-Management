using Ardalis.GuardClauses;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using InventoryManagement.Domain.StockAggregateRoot.Errors;
using InventoryManagement.Domain.StockAggregateRoot.Exceptions;

namespace InventoryManagement.Domain.StockAggregateRoot.Entities;
public class StockItem : Entity<StockItemId>
{
    public ItemId ItemId { get; private set; }
    public int Quantity { get; private set; }
    public int Capacity { get; private set; }

    public StockItem(ItemId itemId,
                     int quantity = 0,
                     int capacity = 0,
                     StockItemId? id = null) : base(id ?? StockItemId.CreateUnique())
    {
        ItemId = itemId;

        Quantity = Guard.Against.Negative(
            quantity, 
            exceptionCreator: () => StockException.Create(StockErrors.InvalidQuantityError));

        Capacity = Guard.Against.Negative(
            capacity,
            exceptionCreator: () => StockException.Create(StockErrors.InvalidQuantityError));
    }
}

