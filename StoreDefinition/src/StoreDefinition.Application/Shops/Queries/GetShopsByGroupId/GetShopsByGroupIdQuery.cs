using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Queries.GetShopsByGroupId;
public sealed record GetShopsByGroupIdQuery(Guid GroupId) : IQuery<Result<IEnumerable<Shop>>>;

