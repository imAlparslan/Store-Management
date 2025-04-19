using Bogus;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductFactories;
internal class ProductCodeFactory
{
    public static ProductCode CreateRandom()
    {
        return new Faker<ProductCode>()
            .CustomInstantiator(faker => new ProductCode(faker.Commerce.Ean8()));
    }
}
