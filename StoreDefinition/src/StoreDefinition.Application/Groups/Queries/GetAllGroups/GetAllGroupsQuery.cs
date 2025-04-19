using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Groups.Queries.GetAllGroups;
public sealed record GetAllGroupsQuery() : IQuery<Result<IEnumerable<Group>>>;