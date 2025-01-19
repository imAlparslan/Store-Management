using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

internal class ShopDescriptionFactory()
{
    internal static ShopDescription CreateValid()
        => new ShopDescription("Valid address description");

    internal static ShopDescription CreateCustom(string description)
        => new ShopDescription(description);
}