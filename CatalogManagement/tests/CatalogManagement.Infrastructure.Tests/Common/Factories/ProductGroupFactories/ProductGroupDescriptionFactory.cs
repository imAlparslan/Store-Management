using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
internal static class ProductGroupDescriptionFactory
{
    public static ProductGroupDescription Create(string description = "group description")
        => new ProductGroupDescription(description);
}
