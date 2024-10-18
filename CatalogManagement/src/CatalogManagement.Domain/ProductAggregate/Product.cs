using CatalogManagement.Domain.Common.Models;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
namespace CatalogManagement.Domain.ProductAggregate;
public sealed class Product : AggregateRoot<ProductId>
{
    public ProductName Name { get; private set; }
    public ProductCode Code { get; private set; }
    public ProductDefinition Definition { get; private set; }

    public Product(ProductName name,
                   ProductCode code,
                   ProductDefinition definition,
                   ProductId? id = null)
                   : base(id ?? ProductId.CreateUnique())
    {
        Name = name;
        Code = code;
        Definition = definition;
    }

    public void ChangeName(ProductName name)
    {
        Name = name;
    }
    public void ChangeCode(ProductCode code)
    {
        Code = code;
    }
    public void ChangeDefinition(ProductDefinition definition)
    {
        Definition = definition;
    }

    private Product()
    {

    }
}

