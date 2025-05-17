namespace InventoryManagement.Domain.Common;
public class ProductId : ValueObject
{
    public Guid Value { get; private set; }

    public ProductId(Guid value)
    {
        Value = value;
    }
    public ProductId(string value)
    {
        Value = Guid.Parse(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(ProductId storeId) => storeId.Value;
    public static implicit operator ProductId(Guid id) => new ProductId(id);
}
