namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
internal class ProductDefinitionFactory
{
    public static ProductDefinition Create(string definition = "product definition")
        => new ProductDefinition(definition);
}
