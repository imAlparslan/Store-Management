using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Groups.Commands.UpdateGroup;
public sealed record UpdateGroupCommand(Guid GroupId, string Name, string Description) : ICommand<Result<Group>>;