using Bogus;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductFactories;
internal class ProductNameFactory
{
    public static ProductName CreateRandom()
    {
        return new Faker<ProductName>()
            .CustomInstantiator(faker => new ProductName(faker.Commerce.ProductName()));
    }
}
