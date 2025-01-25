using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Queries.GetShopById;
public sealed record GetShopByIdQuery(Guid ShopId): IQuery<Result<Shop>>;