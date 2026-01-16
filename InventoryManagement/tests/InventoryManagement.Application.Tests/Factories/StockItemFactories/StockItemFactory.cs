using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Application.Tests.Factories.StockItemFactories;
internal class StockItemFactory
{
    public static StockItem CreateValid(ItemId? itemId = null,
                                        Quantity? quantity = null,
                                        Capacity? capacity = null,
                                        StockItemId? id = null)
        => new StockItem(
            itemId ?? ItemId.CreateUnique(),
            quantity ?? new Quantity(10),
            capacity ?? new Capacity(10),
            id ?? StockItemId.CreateUnique());

}