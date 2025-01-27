using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Infrastructure.Tests.Factories;
public static class ShopFactory
{
    public static Shop CreateValid(ShopId? Id = null)
        => new Shop(ShopDescriptionFactory.CreateValid(), ShopAddressFactory.CreateValid(), [], Id);
    public static Shop CreateCustom(ShopDescription description, ShopAddress address, ShopId? Id = null)
        => new Shop(description, address, [], Id);
    public static Shop CreateWithDescription(string description, ShopId? Id = null)
        => new Shop(new ShopDescription(description), ShopAddressFactory.CreateValid(), [], Id);
    public static Shop CreateWithAddress(ShopAddress address, ShopId? Id = null)
        => new Shop(ShopDescriptionFactory.CreateValid(), address, [], Id);
}
