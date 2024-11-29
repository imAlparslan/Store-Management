﻿using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
internal static class ProductGroupFactory
{
    public static ProductGroup CreateDefault()
    {
        return new ProductGroup(new ProductGroupName("product group name"),
            new ProductGroupDescription("product description"));
    }

    public static ProductGroup CreateRandom()
    {
        var name = ProductGroupNameFactory.CreateRandom();
        var description = ProductGroupDescriptionFactory.CreateRandom();

        return new ProductGroup(name, description);
    }
}
