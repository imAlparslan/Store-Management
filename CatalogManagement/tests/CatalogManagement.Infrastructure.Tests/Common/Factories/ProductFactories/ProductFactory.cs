namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductFactory
{
    public static Product Create()
    {
        var name = ProductNameFactory.Create();
        var code = ProductCodeFactory.Create();
        var definition = ProductDefinitionFactory.Create();

        return new Product(name, code, definition, [], Guid.NewGuid());
    }

}