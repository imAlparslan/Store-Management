using Ardalis.GuardClauses;
using StoreDefinition.Domain.Common.Models;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Exceptions;

namespace StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
public sealed class GroupDescription : ValueObject
{
    public string Value { get; private set; } = null!;

    public GroupDescription(string value)
    {
        Value = Guard.Against
            .NullOrWhiteSpace(
                value,
                exceptionCreator: () => GroupException.Create(GroupErrors.InvalidDescription));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
