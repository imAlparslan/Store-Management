using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.UpdateGroup;
public sealed record UpdateGroupCommand(Guid GroupId, string Name, string Description) : ICommand<Result<Group>>;