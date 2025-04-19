namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public sealed class ProductId : ValueObject
{
    public Guid Value { get; }

    private ProductId(Guid Id)
    {
        Value = Id;
    }

    public static ProductId CreateUnique()
    {
        return new ProductId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static implicit operator Guid(ProductId Id) => Id.Value;
    public static implicit operator ProductId(Guid Id) => new ProductId(Id);

}
