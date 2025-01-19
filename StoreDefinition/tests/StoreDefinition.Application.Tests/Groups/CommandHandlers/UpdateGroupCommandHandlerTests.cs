using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Commands.UpdateGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Exceptions;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Groups.CommandHandlers;
public class UpdateGroupCommandHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly UpdateGroupCommandHandler handler;
    public UpdateGroupCommandHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new UpdateGroupCommandHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsUpdatedGroup_WhenGroupUpdated()
    {
        var group = GroupFactory.Create();
        var command = GroupCommandFactory.GroupUpdateCommand(group.Id);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Should().NotBeNull();
        result.Value!.Should().BeEquivalentTo(group);
        await groupRepository.ReceivedWithAnyArgs(1).UpdateGroupAsync(Arg.Any<Group>());
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(Guid.NewGuid());
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsNullForAnyArgs();

        var result = await handler.Handle(command, default);

        result.IsSuccess!.Should().BeFalse();
        result.Errors!.Should().NotBeNullOrEmpty();
        result.Errors.Should().Contain(GroupErrors.NotFoundById);

    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Handler_ThrowsException_WhenGroupNameInvalid(string invalid)
    {
        var group = GroupFactory.Create();
        var command = GroupCommandFactory.GroupUpdateCommand(group.Id, name: invalid);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            await result.Should().ThrowExactlyAsync<GroupException>();
            await groupRepository.ReceivedWithAnyArgs(0).UpdateGroupAsync(Arg.Any<Group>());
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Handler_ThrowsException_WhenGroupDescriptionInvalid(string invalid)
    {
        var group = GroupFactory.Create();
        var command = GroupCommandFactory.GroupUpdateCommand(group.Id, description: invalid);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            await result.Should().ThrowExactlyAsync<GroupException>();
            await groupRepository.ReceivedWithAnyArgs(0).UpdateGroupAsync(Arg.Any<Group>());
        }
    }

    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
