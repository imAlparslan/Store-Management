using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductFactories;
internal class ProductFactory
{
    public static Product CreateDefault()
    {
        return new Product(new ProductName("product name"),
            new ProductCode("product code"),
            new ProductDefinition("product definition"), []);
    }

    public static Product CreateRandom()
    {
        var name = ProductNameFactory.CreateRandom();
        var code = ProductCodeFactory.CreateRandom();
        var definition = ProductDefinitionFactory.CreateRandom();

        return new Product(name, code, definition, [], Guid.NewGuid());
    }

    public static Product CreateFromCreateCommand(CreateProductCommand command)
    {
        return new Product(new(command.ProductName), new(command.ProductCode), new(command.ProductDefinition), command.GroupIds);
    }

}