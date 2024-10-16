﻿namespace CatalogManagement.Infrastructure.Tests.Common.Factories;
internal class ProductFactory
{
    public static Product CreateDefault()
    {
        return new Product(new ProductName("product name"),
            new ProductCode("product code"),
            new ProductDefinition("product definition"));
    }

    public static Product CreateRandon()
    {
        var name = ProductNameFactory.CreateRandom();
        var code = ProductCodeFactory.CreateRandom();
        var definition = ProductDefinitionFactory.CreateRandom();

        return new Product(name, code, definition, Guid.NewGuid());
    }

}