using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
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

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(group);
        result.Errors.Should().BeNullOrEmpty();
        await groupRepository.ReceivedWithAnyArgs(1).InsertGroupAsync(Arg.Any<Group>());
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Handler_ThrowsShopException_WhenGroupNameInvalid(string invalidName)
    {
        var command = GroupCommandFactory.GroupCreateCommand(name: invalidName);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            await result.Should().ThrowExactlyAsync<GroupException>();
            await groupRepository.ReceivedWithAnyArgs(0).InsertGroupAsync(Arg.Any<Group>());
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Handler_ThrowsShopException_WhenGroupDescriptionInvalid(string invalidDescription)
    {
        var command = GroupCommandFactory.GroupCreateCommand(description: invalidDescription);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            await result.Should().ThrowExactlyAsync<GroupException>();
            await groupRepository.ReceivedWithAnyArgs(0).InsertGroupAsync(Arg.Any<Group>());
        }
    }

    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
