using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Infrastructure.Tests.Factories;

public static class GroupDescriptionFactory
{
    public static GroupDescription CreateValid() => new GroupDescription("valid group description");
    public static GroupDescription CreateCustom(string descrirption)
        => new GroupDescription(descrirption);

}
