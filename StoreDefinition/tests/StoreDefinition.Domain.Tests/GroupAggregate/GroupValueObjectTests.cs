using FluentAssertions;
using StoreDefinition.Domain.Common.Exceptions;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Exceptions;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.Tests.GroupAggregate;
public class GroupValueObjectTests
{
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void CreatingGroupName_ThrowsGroupException_WhenDataInvalid(string name)
    {
        var groupName = () => new GroupName(name);

        groupName.Should().ThrowExactly<GroupException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(GroupErrors.InvalidName.Code);

    }
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void CreatingGroupDescription_ThrowsGroupException_WhenDataInvalid(string description)
    {
        var groupDescription = () => new GroupDescription(description);

        groupDescription.Should().ThrowExactly<GroupException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(GroupErrors.InvalidDescription.Code);

    }

    public static readonly TheoryData<string> invalidStrings = ["", " ", null];
}


