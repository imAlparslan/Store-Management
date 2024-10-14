namespace CatalogManagement.Infrastructure.Tests.Common.Factories;
internal class ProductNameFactory
{
    public static ProductName CreateRandom()
    {
        return new ProductName(Guid.NewGuid().ToString());
    }
}
