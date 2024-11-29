﻿using Bogus;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Application.Tests.Common.Factories.ProductGroupFactories;
internal class ProductGroupNameFactory
{
    public static ProductGroupName CreateRandom()
    {
        return new Faker<ProductGroupName>()
            .CustomInstantiator(faker => new(faker.Commerce.ProductAdjective()));
    }
}
