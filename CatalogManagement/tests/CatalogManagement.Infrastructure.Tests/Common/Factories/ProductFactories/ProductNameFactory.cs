namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductNameFactory
{
    public static ProductName CreateRandom()
    {
        return new ProductName(Guid.NewGuid().ToString());
    }
}
