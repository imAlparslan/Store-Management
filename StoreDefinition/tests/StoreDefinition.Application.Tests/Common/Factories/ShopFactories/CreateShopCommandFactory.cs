using StoreDefinition.Application.Shops.Commands.CreateShop;

namespace StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

internal class CreateShopCommandFactory
{
    internal static CreateShopCommand CreateValid()
        => new CreateShopCommand("valid description", "valid city", "valid street", []);
    internal static CreateShopCommand CreateCustom(string description = "valid description",
                                                   string city = "valid city",
                                                   string street = "valid street")
        => new CreateShopCommand(description, city, street, []);

}
