using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Commands.AddGroupToShop;
public sealed record AddGroupToShopCommand(Guid ShopId, Guid GroupId) : ICommand<Result<Shop>>;
