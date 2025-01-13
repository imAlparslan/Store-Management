using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Infrastructure.Tests.Factories;
public static class ShopDescriptionFactory
{
    public static ShopDescription CreateValid()
        => new ShopDescription("valid description");

    public static ShopDescription CreateCustom(string description)
        => new ShopDescription(description);
}
