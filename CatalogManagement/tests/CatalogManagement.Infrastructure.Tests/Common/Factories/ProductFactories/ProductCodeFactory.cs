namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductCodeFactory
{
    public static ProductCode Create(string code = "code")
        => new ProductCode(code);
}
