namespace InventoryManagement.Domain.StockAggregateRoot.Entities;
public class StockItemId : BaseId
{
    protected StockItemId()
    {
    }

    protected StockItemId(Guid value) : base(value)
    {
    }

    public static StockItemId CreateUnique() => new StockItemId();
    public static implicit operator Guid(StockItemId Id) => Id.Value;
    public static implicit operator StockItemId(Guid Id) => new StockItemId(Id);

}
