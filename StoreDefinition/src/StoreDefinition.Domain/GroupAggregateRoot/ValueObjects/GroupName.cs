using Ardalis.GuardClauses;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Exceptions;

namespace StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
public sealed class GroupName : ValueObject
{
    public string Value { get; private set; }

    public GroupName(string value)
    {
        Value = Guard.Against
            .NullOrWhiteSpace(
                value,
                exceptionCreator: () => GroupException.Create(GroupErrors.InvalidName));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}