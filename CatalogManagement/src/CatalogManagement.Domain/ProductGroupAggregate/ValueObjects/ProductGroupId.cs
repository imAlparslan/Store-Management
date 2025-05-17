namespace CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
public sealed class ProductGroupId : BaseId
{
    private ProductGroupId(Guid id) : base(id)
    {
    }
    private ProductGroupId()
    {
        
    }
    public static ProductGroupId CreateUnique() => new ProductGroupId(Guid.NewGuid());

    public static implicit operator Guid(ProductGroupId productGroupId) => productGroupId.Value;
    public static implicit operator ProductGroupId(Guid productGroupId) => new ProductGroupId(productGroupId);
}
