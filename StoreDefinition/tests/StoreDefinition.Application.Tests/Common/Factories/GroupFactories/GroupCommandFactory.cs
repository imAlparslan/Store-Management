using StoreDefinition.Application.Groups.Commands.CreateGroup;
using StoreDefinition.Application.Groups.Commands.UpdateGroup;

namespace StoreDefinition.Application.Tests.Common.Factories.GroupFactories;

internal class GroupCommandFactory
{
    internal static CreateGroupCommand GroupCreateCommand(string name = "valid group name", string description = "valid group description")
        => new CreateGroupCommand(name, description);

    internal static UpdateGroupCommand GroupUpdateCommand(Guid id, string name = "updated group name", string description = "updated group description")
        => new UpdateGroupCommand(id, name, description);
}