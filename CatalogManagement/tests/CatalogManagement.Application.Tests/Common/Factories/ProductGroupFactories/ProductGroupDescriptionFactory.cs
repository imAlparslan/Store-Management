using Bogus;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductGroupFactories;
internal class ProductGroupDescriptionFactory
{
    public static ProductGroupDescription CreateRandom()
    {
        return new Faker<ProductGroupDescription>()
            .CustomInstantiator(faker => new(faker.Commerce.ProductDescription()));
    }
}
