using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.ItemAggregateRoot;
public class Item : AggregateRoot<ItemId>
{
    public ProductId ProductId { get; private set; }
    public ProductDefinition ProductDefinition { get; private set; }
    public bool IsDefaultStockItem { get; private set; }

    public Item(ProductId productId,
                ProductDefinition productDefinition,
                bool isDefaultStockItem,
                ItemId? id = null) :base(id ?? ItemId.CreateUnique())
    {
        ProductId = productId;
        ProductDefinition = productDefinition;
        IsDefaultStockItem = isDefaultStockItem;
    }
    private Item()
    {
    }
}
