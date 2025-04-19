namespace StoreDefinition.Application.Groups.Commands.DeleteGroup;
public sealed record DeleteGroupCommand(Guid GroupId) : ICommand<Result<bool>>;
