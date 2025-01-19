using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.DeleteGroup;
public sealed record DeleteGroupCommand(Guid GroupId) : ICommand<Result<bool>>;
