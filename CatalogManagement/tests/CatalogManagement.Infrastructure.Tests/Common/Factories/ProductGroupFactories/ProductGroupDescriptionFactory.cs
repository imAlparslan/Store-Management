using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
internal static class ProductGroupDescriptionFactory
{
    public static ProductGroupDescription CreateRandom()
    {
        return new ProductGroupDescription(Guid.NewGuid().ToString());
    }
}
