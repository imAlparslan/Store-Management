using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Infrastructure.Tests.Factories;

public static class GroupNameFactory
{
    public static GroupName CreateValid() => new GroupName("valid group name");
    public static GroupName CreateCustom(string groupName) => new GroupName(groupName);
}