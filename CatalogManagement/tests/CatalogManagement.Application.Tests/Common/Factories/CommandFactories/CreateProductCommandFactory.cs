using Bogus;
using CatalogManagement.Application.Products;

namespace CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
internal class CreateProductCommandFactory
{

    public static CreateProductCommand CreateCustom(string productName, string productCode, string productDefinition)
    {
        return new Faker<CreateProductCommand>()
            .CustomInstantiator(x => new(productName, productCode, productDefinition))
            .Generate();
    }
    public static CreateProductCommand CreateValid()
    {
        return new Faker<CreateProductCommand>()
             .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.Ean8(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static CreateProductCommand CreateWithName(string productName)
    {
        return new Faker<CreateProductCommand>()
            .CustomInstantiator(x => new(productName, x.Commerce.Ean8(), x.Commerce.ProductDescription()))
            .Generate();
    }
    public static CreateProductCommand CreateWithCode(string productCode)
    {
        return new Faker<CreateProductCommand>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), productCode, x.Commerce.ProductDescription()))
            .Generate();
    }

    public static CreateProductCommand CreateWithDefinition(string productDefinition)
    {
        return new Faker<CreateProductCommand>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.Ean8(), productDefinition))
            .Generate();
    }
}
