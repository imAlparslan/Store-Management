using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Commands.UpdateShop;
public sealed record UpdateShopCommand(Guid ShopId, string Description, string City, string Street)
    : ICommand<Result<Shop>>;