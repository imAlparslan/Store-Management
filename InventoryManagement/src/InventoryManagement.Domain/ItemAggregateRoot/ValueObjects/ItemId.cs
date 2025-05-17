using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;

namespace InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
public class ItemId : BaseId
{
    protected ItemId()
    {
    }

    protected ItemId(Guid value) : base(value)
    {
    }
    public static ItemId CreateUnique() => new ItemId();
    public static implicit operator Guid(ItemId Id) => Id.Value;
    public static implicit operator ItemId(Guid Id) => new ItemId(Id);
}
