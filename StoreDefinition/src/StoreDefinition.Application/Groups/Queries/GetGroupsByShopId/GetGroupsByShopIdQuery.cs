using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Queries.GetGroupsByShopId;
public sealed record GetGroupsByShopIdQuery(Guid ShopId) : IQuery<Result<IEnumerable<Group>>>;