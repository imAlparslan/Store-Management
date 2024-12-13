using CatalogManagement.Domain.Common.Models;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Domain.ProductGroupAggregate;
public sealed class ProductGroup : AggregateRoot<ProductGroupId>
{
    public ProductGroupName Name { get; private set; }
    public ProductGroupDescription Description { get; private set; }

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

    public void AddProduct(Guid productId)
    {
        if (!_productIds.Contains(productId))
        {
            _productIds.Add(productId);
        }
    }
    public void RemoveProduct(Guid productId)
    {
        if (_productIds.Contains(productId))
        {
            _productIds.Remove(productId);
        }
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
