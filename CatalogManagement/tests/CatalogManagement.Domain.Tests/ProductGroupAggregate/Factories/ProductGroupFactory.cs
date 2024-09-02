using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Domain.Tests.ProductGroupAggregate.Factories;
public static class ProductGroupFactory
{
    public static ProductGroup Create()
    {
        return new ProductGroup(new ProductGroupName("product group name"),
                                new ProductGroupDescription("product group description"));
    }
}
