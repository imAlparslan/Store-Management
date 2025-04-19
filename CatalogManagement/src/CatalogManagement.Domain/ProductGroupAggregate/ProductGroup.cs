using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Domain.ProductGroupAggregate;
public sealed class ProductGroup : AggregateRoot<ProductGroupId>
{
    public ProductGroupName Name { get; private set; } = null!;
    public ProductGroupDescription Description { get; private set; } = null!;

    private readonly List<Guid> _productIds = new();
    public IReadOnlyList<Guid> ProductIds => _productIds;

    public ProductGroup(ProductGroupName name,
                        ProductGroupDescription description,
                        ProductGroupId? id = null)
                        : base(id ?? ProductGroupId.CreateUnique())
    {
        Description = description;
        Name = name;
    }

    public bool AddProduct(Guid productId)
    {
        if (!HasProduct(productId))
        {
            _productIds.Add(productId);
            return true;
        }
        return false;
    }
    public bool RemoveProduct(Guid productId)
    {
        if (HasProduct(productId))
        {
            return _productIds.Remove(productId);
        }
        return false;
    }
    public bool HasProduct(Guid productId)
    {
        return _productIds.Contains(productId);
    }
    public void ChangeName(ProductGroupName name)
    {
        Name = name;
    }
    public void ChangeDescription(ProductGroupDescription description)
    {
        Description = description;
    }
    private ProductGroup()
    {

    }

}
