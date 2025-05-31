using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Exceptions;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.Tests.GroupAggregate;

public class GroupValueObjectTests
{
    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void CreatingGroupName_ThrowsGroupException_WhenDataInvalid(string name)
    {
        var groupName = () => new GroupName(name);

        groupName.ShouldThrow<GroupException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(GroupErrors.InvalidName.Code),
                ex => ex.Message.ShouldBe(GroupErrors.InvalidName.Description),
                ex => ex.ShouldBeAssignableTo<DomainException>()
            );
    }
    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void CreatingGroupDescription_ThrowsGroupException_WhenDataInvalid(string description)
    {
        var groupDescription = () => new GroupDescription(description);

        groupDescription.ShouldThrow<GroupException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(GroupErrors.InvalidDescription.Code),
                ex => ex.Message.ShouldBe(GroupErrors.InvalidDescription.Description),
                ex => ex.ShouldBeAssignableTo<DomainException>()
            );
    }
}