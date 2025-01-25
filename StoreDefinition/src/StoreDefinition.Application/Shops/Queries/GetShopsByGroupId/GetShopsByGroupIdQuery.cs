using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Shops.Queries.GetShopsByGroupId;
public sealed record GetShopsByGroupIdQuery(Guid GroupId): IQuery<Result<IEnumerable<Shop>>>;