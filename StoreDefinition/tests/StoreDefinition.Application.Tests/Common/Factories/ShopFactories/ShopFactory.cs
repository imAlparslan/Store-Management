using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;

namespace StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

internal class ShopFactory
{
    internal static Shop CreateValid(Guid? shopId = null, Guid? addressId = null)
        => new Shop(
            ShopDescriptionFactory.CreateValid(),
            ShopAddressFactory.CreateValid(addressId),
            [],
            shopId);

    internal static Shop CreateCustom(string description, string city, string street, Guid? shopId = null, Guid? addressId = null)
        => new Shop(ShopDescriptionFactory.CreateCustom(description),
            ShopAddressFactory.CreateCustom(city, street, addressId),
            [],
            shopId);

    internal static Shop CreateWithDescription(string description, Guid? shopId = null, Guid? addressId = null)
        => new Shop(
            ShopDescriptionFactory.CreateCustom(description),
            ShopAddressFactory.CreateValid(addressId),
            [],
            shopId);

    internal static Shop CreateWithAddress(ShopAddress address, Guid? shopId = null)
        => new Shop(
            ShopDescriptionFactory.CreateValid(),
            address,
            [],
            shopId);
}
