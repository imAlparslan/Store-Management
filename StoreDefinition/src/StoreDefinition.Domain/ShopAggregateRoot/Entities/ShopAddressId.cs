namespace StoreDefinition.Domain.ShopAggregateRoot.Entities;
public class ShopAddressId : ValueObject
{
    public Guid Value { get; }
    private ShopAddressId(Guid value)
    {
        Value = value;
    }

    public static ShopAddressId CreateUnique()
    {
        return new ShopAddressId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


    public static implicit operator Guid(ShopAddressId shopId) => shopId.Value;

    public static implicit operator ShopAddressId(Guid shopId) => new ShopAddressId(shopId);
}
