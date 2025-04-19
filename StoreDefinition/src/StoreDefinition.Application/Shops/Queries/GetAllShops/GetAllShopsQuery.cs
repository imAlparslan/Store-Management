using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Queries.GetAllShops;

public sealed record GetAllShopsQuery() : IQuery<Result<IEnumerable<Shop>>>;

