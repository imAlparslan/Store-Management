using Bogus;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.RequestFactories;
internal class CreateProductGroupRequestFactory
{
    public static CreateProductGroupRequest CreateCustom(string productGroupName, string productGroupDescription)
    {
        return new Faker<CreateProductGroupRequest>()
            .CustomInstantiator(x => new(productGroupName, productGroupDescription))
            .Generate();
    }
    public static CreateProductGroupRequest CreateValid()
    {
        return new Faker<CreateProductGroupRequest>()
             .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static CreateProductGroupRequest CreateWithName(string productGroupName)
    {
        return new Faker<CreateProductGroupRequest>()
            .CustomInstantiator(x => new(productGroupName, x.Commerce.ProductDescription()))
            .Generate();
    }

    public static CreateProductGroupRequest CreateWithDescription(string productGropDescription)
    {
        return new Faker<CreateProductGroupRequest>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), productGropDescription))
            .Generate();
    }
}
