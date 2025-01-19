using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.AddGroupToShop;
public sealed record AddGroupToShopCommand(Guid ShopId, Guid GroupId) : ICommand<Result<Shop>>;
