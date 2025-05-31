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

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeEquivalentTo(group);
        await groupRepository.ReceivedWithAnyArgs(1).UpdateGroupAsync(Arg.Any<Group>());
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(Guid.NewGuid());
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsNullForAnyArgs();

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(GroupErrors.NotFoundById);

    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Handler_ThrowsException_WhenGroupNameInvalid(string invalid)
    {
        var group = GroupFactory.Create();
        var command = GroupCommandFactory.GroupUpdateCommand(group.Id, name: invalid);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);

        var result = () => handler.Handle(command, default);

        await result.ShouldThrowAsync<GroupException>();
        await groupRepository.ReceivedWithAnyArgs(0).UpdateGroupAsync(Arg.Any<Group>());
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Handler_ThrowsException_WhenGroupDescriptionInvalid(string invalid)
    {
        var group = GroupFactory.Create();
        var command = GroupCommandFactory.GroupUpdateCommand(group.Id, description: invalid);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);

        var result = () => handler.Handle(command, default);

        await result.ShouldThrowAsync<GroupException>();
        await groupRepository.ReceivedWithAnyArgs(0).UpdateGroupAsync(Arg.Any<Group>());
    }
}
