using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.RemoveGroupFromShop;
public sealed record RemoveGroupFromShopCommand(Guid ShopId, Guid GroupId) : ICommand<Result<Shop>>;
