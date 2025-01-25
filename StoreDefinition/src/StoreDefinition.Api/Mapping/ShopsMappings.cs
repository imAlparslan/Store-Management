using StoreDefinition.Application.Shops.Commands.AddGroupToShop;
using StoreDefinition.Application.Shops.Commands.CreateShop;
using StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;
using StoreDefinition.Application.Shops.Commands.UpdateShop;
using StoreDefinition.Contracts.Shops;
using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Api.Mapping;

public static class ShopsMappings
{
    public static CreateShopCommand MapToCommand(this CreateShopRequest request)
        => new CreateShopCommand(request.Description, request.City, request.Street);

    public static UpdateShopCommand MapToCommand(this UpdateShopRequest request, Guid id)
        => new UpdateShopCommand(id, request.Description, request.City, request.Street);

    public static AddGroupToShopCommand MapToCommand(this AddGroupToShopRequest request, Guid id)
        => new AddGroupToShopCommand(id, request.GroupId);

    public static RemoveGroupFromShopCommand MapToCommand(this RemoveGroupFromShopRequest request, Guid id)
        => new RemoveGroupFromShopCommand(id, request.GroupId);

    public static ShopResponse MapToResponse(this Shop shop)
        => new ShopResponse(shop.Id, shop.Description.Value, shop.Address.City, shop.Address.Street, shop.GroupIds);
}
