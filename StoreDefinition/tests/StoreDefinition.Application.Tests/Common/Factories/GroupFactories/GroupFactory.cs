using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
internal class GroupFactory
{
    internal static Group Create(string name = "valid group name", string description = "valid group description", Guid? id = null)
        => new Group(
            GroupNameFactory.CreateCustom(name),
            GroupDescriptionFactory.CreateCustom(description),
            id);
}
