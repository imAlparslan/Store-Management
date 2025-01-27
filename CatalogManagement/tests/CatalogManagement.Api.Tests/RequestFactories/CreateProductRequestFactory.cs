using Bogus;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.RequestFactories;
public static class CreateProductRequestFactory
{

    public static CreateProductRequest CreateCustom(string productName, string productCode, string productDefinition)
    {
        return new Faker<CreateProductRequest>()
            .CustomInstantiator(x => new(productName, productCode, productDefinition, []))
            .Generate();
    }
    public static CreateProductRequest CreateValid()
    {
        return new Faker<CreateProductRequest>()
             .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.Ean8(), x.Commerce.ProductDescription(), []))
             .Generate();
    }

    public static CreateProductRequest CreateWithName(string productName)
    {
        return new Faker<CreateProductRequest>()
            .CustomInstantiator(x => new(productName, x.Commerce.Ean8(), x.Commerce.ProductDescription(), []))
            .Generate();
    }
    public static CreateProductRequest CreateWithCode(string productCode)
    {
        return new Faker<CreateProductRequest>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), productCode, x.Commerce.ProductDescription(), []))
            .Generate();
    }

    public static CreateProductRequest CreateWithDefinition(string productDefinition)
    {
        return new Faker<CreateProductRequest>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.Ean8(), productDefinition, []))
            .Generate();
    }
}
