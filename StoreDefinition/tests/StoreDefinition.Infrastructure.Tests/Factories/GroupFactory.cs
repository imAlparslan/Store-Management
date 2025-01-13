
using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Infrastructure.Tests.Factories;
public static class GroupFactory
{

    public static Group CreateValid(Guid? Id = null)
        => new Group(GroupNameFactory.CreateValid(), GroupDescriptionFactory.CreateValid(), Id);

    public static Group CreateWithName(string name, Guid? Id)
        => new Group(GroupNameFactory.CreateCustom(name), GroupDescriptionFactory.CreateValid(), Id);

    public static Group CreateWithDescription(string description, Guid? Id)
        => new Group(GroupNameFactory.CreateValid(), GroupDescriptionFactory.CreateCustom(description), Id);

    public static Group CreateCustom(string name, string description, Guid? Id)
        => new Group(GroupNameFactory.CreateCustom(name), GroupDescriptionFactory.CreateCustom(description), Id);
}
