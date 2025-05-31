using Bogus;

namespace CatalogManagement.Api.Tests.RequestFactories;
internal static class UpdateProductRequestFactory
{
    public static UpdateProductRequest CreateCustom(string productName, string productCode, string productDefinition)
    {
        return new Faker<UpdateProductRequest>()
            .CustomInstantiator(x => new(productName, productCode, productDefinition))
            .Generate();
    }
    public static UpdateProductRequest CreateValid()
    {
        return new Faker<UpdateProductRequest>()
             .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.Ean8(), x.Commerce.ProductDescription()))
             .Generate();
    }

    public static UpdateProductRequest CreateWithName(string productName)
    {
        return new Faker<UpdateProductRequest>()
            .CustomInstantiator(x => new(productName, x.Commerce.Ean8(), x.Commerce.ProductDescription()))
            .Generate();
    }
    public static UpdateProductRequest CreateWithCode(string productCode)
    {
        return new Faker<UpdateProductRequest>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), productCode, x.Commerce.ProductDescription()))
            .Generate();
    }

    public static UpdateProductRequest CreateWithDefinition(string productDefinition)
    {
        return new Faker<UpdateProductRequest>()
            .CustomInstantiator(x => new(x.Commerce.ProductName(), x.Commerce.Ean8(), productDefinition))
            .Generate();
    }
}
