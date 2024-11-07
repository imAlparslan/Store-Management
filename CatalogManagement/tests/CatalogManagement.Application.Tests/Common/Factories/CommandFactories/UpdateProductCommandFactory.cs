using Bogus;
using CatalogManagement.Application.Products;

namespace CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
public static class UpdateProductCommandFactory
{
    public static UpdateProductCommand CreateCustom(string productName, string productCode, string productDefinition)
    {
        return new Faker<UpdateProductCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), productName, productCode, productDefinition))
            .Generate();
    }

    public static UpdateProductCommand CreateCustom(Guid id, string productName, string productCode, string productDefinition)
    {
        return new Faker<UpdateProductCommand>()
            .CustomInstantiator(x => new(id, productName, productCode, productDefinition))
            .Generate();
    }

    public static UpdateProductCommand CreateValid()
    {
        return new Faker<UpdateProductCommand>()
             .CustomInstantiator(x => new(Guid.NewGuid(), x.Commerce.ProductName(), x.Commerce.Ean8(), x.Commerce.ProductDescription()))
             .Generate();
    }
    public static UpdateProductCommand CreateWithId(Guid id)
    {
        return new Faker<UpdateProductCommand>()
             .CustomInstantiator(x => new(id, x.Commerce.ProductName(), x.Commerce.Ean8(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static UpdateProductCommand CreateWithName(string productName)
    {
        return new Faker<UpdateProductCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), productName, x.Commerce.Ean8(), x.Commerce.ProductDescription()))
            .Generate();
    }
    public static UpdateProductCommand CreateWithCode(string productCode)
    {
        return new Faker<UpdateProductCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), x.Commerce.ProductName(), productCode, x.Commerce.ProductDescription()))
            .Generate();
    }

    public static UpdateProductCommand CreateWithDefinition(string productDefinition)
    {
        return new Faker<UpdateProductCommand>()
            .CustomInstantiator(x => new(Guid.NewGuid(), x.Commerce.ProductName(), x.Commerce.Ean8(), productDefinition))
            .Generate();
    }
}
