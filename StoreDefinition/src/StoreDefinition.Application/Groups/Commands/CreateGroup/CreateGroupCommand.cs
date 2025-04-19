using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Groups.Commands.CreateGroup;
public sealed record CreateGroupCommand(string Name, string Description) : ICommand<Result<Group>>;