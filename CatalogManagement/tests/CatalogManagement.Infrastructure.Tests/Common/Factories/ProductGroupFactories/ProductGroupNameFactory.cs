using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
internal static class ProductGroupNameFactory
{
    public static ProductGroupName Create(string name = "group name")
        => new ProductGroupName(name);
}
