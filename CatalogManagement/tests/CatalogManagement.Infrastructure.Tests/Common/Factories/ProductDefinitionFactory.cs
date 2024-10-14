namespace CatalogManagement.Infrastructure.Tests.Common.Factories;
internal class ProductDefinitionFactory
{
    public static ProductDefinition CreateRandom()
    {
        return new ProductDefinition(Guid.NewGuid().ToString());
    }
}
