using CatalogManagement.Domain.Common.Models;

namespace CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
public sealed class ProductGroupId : ValueObject
{
    public Guid Value { get; }

    private ProductGroupId(Guid Id)
    {
        Value = Id;
    }
    public static ProductGroupId CreateUnique()
    {
        return new ProductGroupId(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


    public static implicit operator Guid(ProductGroupId productGroupId) => productGroupId.Value;
    public static implicit operator ProductGroupId(Guid productGroupId) => new ProductGroupId(productGroupId);
}
