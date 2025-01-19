using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Common.Factories.GroupFactories;

internal class GroupNameFactory
{
    internal static GroupName CreateValid() => new("valid group name");
    internal static GroupName CreateCustom(string name) => new(name);
}
