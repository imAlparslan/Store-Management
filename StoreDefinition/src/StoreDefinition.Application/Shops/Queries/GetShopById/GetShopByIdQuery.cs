using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Shops.Queries.GetShopById;

public sealed record GetShopByIdQuery(Guid ShopId) : IQuery<Result<Shop>>;

