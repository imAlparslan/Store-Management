using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Common.Factories.GroupFactories;

internal class GroupDescriptionFactory
{
    internal static GroupDescription CreateValid() => new("valid group description");
    internal static GroupDescription CreateCustom(string name) => new(name);
}