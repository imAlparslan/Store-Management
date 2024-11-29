using Bogus;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.RequestFactories;
internal class UpdateProductGroupRequestFactory
{
    public static UpdateProductGroupRequest CreateCustom(string productGroupName, string productGroupDescription)
    {
        return new Faker<UpdateProductGroupRequest>()
            .CustomInstantiator(x => new(productGroupName, productGroupDescription))
            .Generate();
    }
    public static UpdateProductGroupRequest CreateValid()
    {
        return new Faker<UpdateProductGroupRequest>()
             .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static UpdateProductGroupRequest CreateWithName(string productGroupName)
    {
        return new Faker<UpdateProductGroupRequest>()
            .CustomInstantiator(x => new(productGroupName, x.Commerce.ProductDescription()))
            .Generate();
    }

    public static UpdateProductGroupRequest CreateWithDescription(string productGropDescription)
    {
        return new Faker<UpdateProductGroupRequest>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), productGropDescription))
            .Generate();
    }
}
