using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.UpdateShop;
public sealed record UpdateShopCommand(Guid ShopId, string Description, string City, string Street)
    : ICommand<Result<Shop>>;