namespace InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
public sealed class StockId : BaseId
{
    private StockId(Guid Id) : base(Id)
    {
    }
    private StockId()
    {
    }
    public static StockId CreateUnique() => new StockId();
    public static implicit operator Guid(StockId Id) => Id.Value;
    public static implicit operator StockId(Guid Id) => new StockId(Id);

}