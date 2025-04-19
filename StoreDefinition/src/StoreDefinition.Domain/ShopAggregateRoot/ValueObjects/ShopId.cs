namespace StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
public sealed class ShopId : ValueObject
{
    public Guid Value { get; }
    private ShopId(Guid value)
    {
        Value = value;
    }

    public static ShopId CreateUnique()
    {
        return new ShopId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


    public static implicit operator Guid(ShopId shopId) => shopId.Value;

    public static implicit operator ShopId(Guid shopId) => new ShopId(shopId);
}
