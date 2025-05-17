using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.ItemAggregateRoot;
public class Item : AggregateRoot<ItemId>
{
    public ProductId ProductId { get; set; }
    public ProductDefinition ProductDefinition { get; set; }
    public bool IsDefaultStockItem { get; set; }

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
