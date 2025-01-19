using StoreDefinition.Domain.ShopAggregateRoot.Entities;

namespace StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

internal class ShopAddressFactory
{
    internal static ShopAddress CreateValid(Guid? id = null)
        => new ShopAddress("valid city", "valid street", id ?? Guid.NewGuid());

    internal static ShopAddress CreateCustom(string city, string street, Guid? id = null)
        => new ShopAddress(city, street, id ?? Guid.NewGuid());

    internal static ShopAddress CreateWithCity(string city, Guid? id = null)
        => new ShopAddress(city, "valid street", id ?? Guid.NewGuid());

    internal static ShopAddress CreateWithStreet(string street, Guid? id = null)
        => new ShopAddress("valid city", street, id ?? Guid.NewGuid());
}
