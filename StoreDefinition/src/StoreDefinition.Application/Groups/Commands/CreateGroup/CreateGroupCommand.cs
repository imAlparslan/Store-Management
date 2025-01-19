using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.CreateGroup;
public sealed record CreateGroupCommand(string Name, string Description) : ICommand<Result<Group>>;