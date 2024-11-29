using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
internal static class ProductGroupNameFactory
{
    public static ProductGroupName CreateRandom()
    {
        return new ProductGroupName(Guid.NewGuid().ToString());
    }
}
