using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Groups.Queries.GetGroupById;
public sealed record GetGroupByIdQuery(Guid GroupId) : IQuery<Result<Group>>;
