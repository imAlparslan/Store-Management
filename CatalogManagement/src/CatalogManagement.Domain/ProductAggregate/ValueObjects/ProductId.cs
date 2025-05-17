namespace CatalogManagement.Domain.ProductAggregate.ValueObjects;
public sealed class ProductId : BaseId
{
    private ProductId(Guid id) : base(id)
    {
    }
    private ProductId()
    {

    }

    public static ProductId CreateUnique() => new ProductId();

    public static implicit operator Guid(ProductId id) => id.Value;
    public static implicit operator ProductId(Guid id) => new ProductId(id);

}
