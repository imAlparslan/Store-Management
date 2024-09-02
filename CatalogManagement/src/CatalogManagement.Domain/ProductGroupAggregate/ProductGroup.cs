using CatalogManagement.Domain.Common.Models;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Domain.ProductGroupAggregate;
public sealed class ProductGroup : AggregateRoot<ProductGroupId>
{
    public ProductGroupName Name { get; private set; }
    public ProductGroupDescription Description { get; private set; }

    public ProductGroup(ProductGroupName name,
                        ProductGroupDescription description,
                        ProductGroupId? id = null)
        : base(id ?? ProductGroupId.CreateUnique())
    {
        Description = description;
        Name = name;
    }

}
