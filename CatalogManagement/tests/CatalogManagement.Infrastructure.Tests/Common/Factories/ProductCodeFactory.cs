namespace CatalogManagement.Infrastructure.Tests.Common.Factories;
internal class ProductCodeFactory
{

    public static ProductCode CreateRandom()
    {
        return new ProductCode(Guid.NewGuid().ToString());
    }
}
