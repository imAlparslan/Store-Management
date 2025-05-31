using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Commands.CreateGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Exceptions;

namespace StoreDefinition.Application.Tests.Groups.CommandHandlers;

public class CreateGroupCommandHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly CreateGroupCommandHandler handler;
    public CreateGroupCommandHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new CreateGroupCommandHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsCreatedGroup_WhenGroupCreated()
    {
        var command = GroupCommandFactory.GroupCreateCommand();
        var group = GroupFactory.Create(command.Name, command.Description);
        groupRepository.InsertGroupAsync(group).ReturnsForAnyArgs(group);

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(group);
        result.Errors.ShouldBeNull();
        await groupRepository.ReceivedWithAnyArgs(1).InsertGroupAsync(Arg.Any<Group>());
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Handler_ThrowsShopException_WhenGroupNameInvalid(string invalidName)
    {
        var command = GroupCommandFactory.GroupCreateCommand(name: invalidName);

        var result = () => handler.Handle(command, default);

        await result.ShouldThrowAsync<GroupException>();
        await groupRepository.ReceivedWithAnyArgs(0).InsertGroupAsync(Arg.Any<Group>());
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Handler_ThrowsShopException_WhenGroupDescriptionInvalid(string invalidDescription)
    {
        var command = GroupCommandFactory.GroupCreateCommand(description: invalidDescription);

        var result = () => handler.Handle(command, default);

        await result.ShouldThrowAsync<GroupException>();
        await groupRepository.ReceivedWithAnyArgs(0).InsertGroupAsync(Arg.Any<Group>());
    }
}
