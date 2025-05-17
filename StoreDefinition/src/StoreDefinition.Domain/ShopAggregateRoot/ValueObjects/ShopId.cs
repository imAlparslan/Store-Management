namespace StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
public sealed class ShopId : BaseId
{
    private ShopId(Guid value) : base(value)
    {
    }
    private ShopId()
    {
        
    }
    public static ShopId CreateUnique()
    {
        return new ShopId();
    }
    public static ShopId CreateFromString(string value) => new ShopId(Guid.Parse(value));

    public static implicit operator Guid(ShopId shopId) => shopId.Value;

    public static implicit operator ShopId(Guid shopId) => new ShopId(shopId);
}
