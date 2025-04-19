using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
internal static class ProductGroupFactory
{
    public static ProductGroup Create()
    {
        var name = ProductGroupNameFactory.Create();
        var description = ProductGroupDescriptionFactory.Create();

        return new ProductGroup(name, description);
    }
}
