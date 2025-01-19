using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Queries.GetAllGroups;
public sealed record GetAllGroupsQuery() : IQuery<Result<IEnumerable<Group>>>;