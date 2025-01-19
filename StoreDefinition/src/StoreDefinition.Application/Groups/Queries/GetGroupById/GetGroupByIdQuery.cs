using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Queries.GetGroupById;
public sealed record GetGroupByIdQuery(Guid GroupId) : IQuery<Result<Group>>;
