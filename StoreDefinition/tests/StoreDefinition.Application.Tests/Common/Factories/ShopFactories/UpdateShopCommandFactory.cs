using StoreDefinition.Application.Shops.Commands.UpdateShop;

namespace StoreDefinition.Application.Tests.Common.Factories.ShopFactories;

internal class UpdateShopCommandFactory
{
    private UpdateShopCommandFactory()
    {
        
    }
    internal static UpdateShopCommand CreateValid(Guid? shopId = null)
        => new UpdateShopCommand(shopId ?? Guid.NewGuid(), "valid description", "valid city", "valid street");
    internal static UpdateShopCommand CreateCustom(string description = "valid description",
                                                   string city = "valid city",
                                                   string street = "valid street",
                                                   Guid? shopId = null)
        => new UpdateShopCommand(shopId ?? Guid.NewGuid(), description, city, street);

}