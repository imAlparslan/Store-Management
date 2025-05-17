namespace InventoryManagement.Domain.Common;
public class StoreId : ValueObject
{
    public Guid Value { get; private set; }

    public StoreId(Guid value)
    {
        Value = value;
    }
    public StoreId(string value)
    {
        Value = Guid.Parse(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(StoreId storeId) => storeId.Value;
    public static implicit operator StoreId(Guid id) => new StoreId(id);
}
