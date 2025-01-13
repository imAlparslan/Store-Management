using StoreDefinition.Domain.ShopAggregateRoot.Entities;

namespace StoreDefinition.Infrastructure.Tests.Factories;
public static class ShopAddressFactory
{
    public static ShopAddress CreateValid(ShopAddressId? Id = null)
        => new ShopAddress("valid city", "valid street", Id);

    public static ShopAddress CreateCustom(string city, string street, ShopAddressId? Id = null)
        => new ShopAddress(city, street, Id);

    public static ShopAddress CreateWithCity(string city, ShopAddressId? Id = null)
        => new ShopAddress(city, "valid street", Id);

    public static ShopAddress CreateWithStreet(string street, ShopAddressId? Id = null)
        => new ShopAddress("valid city", street, Id);
}
