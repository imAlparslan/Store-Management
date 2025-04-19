using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Groups.Queries.GetGroupsByShopId;
public sealed record GetGroupsByShopIdQuery(Guid ShopId) : IQuery<Result<IEnumerable<Group>>>;