using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;

public sealed record RemoveGroupFromShopCommand(Guid ShopId, Guid GroupId) : ICommand<Result<Shop>>;
