using Bogus;

namespace CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
internal class CreateProductGroupCommandFactory
{

    public static CreateProductGroupCommand CreateCustom(string productGroupName, string productGroupDefinition)
    {
        return new Faker<CreateProductGroupCommand>()
            .CustomInstantiator(x => new(productGroupName, productGroupDefinition))
            .Generate();
    }
    public static CreateProductGroupCommand CreateValid()
    {
        return new Faker<CreateProductGroupCommand>()
             .CustomInstantiator(x => new(x.Commerce.ProductAdjective(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static CreateProductGroupCommand CreateWithName(string productGroupName)
    {
        return new Faker<CreateProductGroupCommand>()
            .CustomInstantiator(x => new(productGroupName, x.Commerce.ProductDescription()))
            .Generate();
    }

    public static CreateProductGroupCommand CreateWithDefinition(string productGroupDefinition)
    {
        return new Faker<CreateProductGroupCommand>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), productGroupDefinition))
            .Generate();
    }
}
