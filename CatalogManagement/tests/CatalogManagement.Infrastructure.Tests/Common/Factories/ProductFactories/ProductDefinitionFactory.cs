namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductDefinitionFactory
{
    public static ProductDefinition CreateRandom()
    {
        return new ProductDefinition(Guid.NewGuid().ToString());
    }
}
