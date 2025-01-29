using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;

namespace CatalogManagement.Domain.Tests.ProductAggregate.Factories;
public static class ProductFactory
{
    public static Product Create(List<Guid>? groupIds = null)
    {
        return new Product(new ProductName("product name"),
                           new ProductCode("product code"),
                           new ProductDefinition("product definition"),
                           groupIds ?? new List<Guid>());
    }
}
