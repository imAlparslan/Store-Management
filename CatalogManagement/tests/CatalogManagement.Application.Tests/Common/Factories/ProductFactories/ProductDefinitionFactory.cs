using Bogus;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductFactories;
internal class ProductDefinitionFactory
{
    public static ProductDefinition CreateRandom()
    {
        return new Faker<ProductDefinition>()
            .CustomInstantiator(faker => new ProductDefinition(faker.Commerce.ProductDescription()));
    }
}
