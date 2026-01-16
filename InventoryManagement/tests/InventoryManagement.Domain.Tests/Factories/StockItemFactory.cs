
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.Tests.Factories;

public static class StockItemFactory
{
    public static StockItem CreateDefault(int initialCapacity = 10, int initialQuantity = 0)
    {
        return new StockItem(
            Guid.CreateVersion7(),
            new Quantity(initialQuantity),
            new Capacity(initialCapacity)
        );
    }
}