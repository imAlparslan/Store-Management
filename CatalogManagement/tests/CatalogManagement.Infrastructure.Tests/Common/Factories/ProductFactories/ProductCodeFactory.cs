namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductCodeFactory
{

    public static ProductCode CreateRandom()
    {
        return new ProductCode(Guid.NewGuid().ToString());
    }
}
