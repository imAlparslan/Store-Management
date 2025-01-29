namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductNameFactory
{
    public static ProductName Create(string name = "product name")
        => new ProductName(name);
}
