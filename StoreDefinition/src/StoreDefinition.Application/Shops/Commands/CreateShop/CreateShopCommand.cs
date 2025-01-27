using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Commands.CreateShop;
public sealed record CreateShopCommand(string Description,
                                       string City,
                                       string Street,
                                       IReadOnlyList<Guid> GroupIds): ICommand<Result<Shop>>;