using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Commands.DeleteGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Groups.CommandHandlers;
public class DeleteGroupCommandHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly DeleteGroupCommandHandler handler;
    public DeleteGroupCommandHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new DeleteGroupCommandHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsTrue_WhenGroupDeleted()
    {
        var group = GroupFactory.Create();
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        var command = new DeleteGroupCommand(group.Id);

        var result = await handler.Handle(command,default);

        result.IsSuccess.Should().BeTrue();
        await groupRepository.ReceivedWithAnyArgs(1).DeleteGroupByIdAsync(Arg.Any<GroupId>());
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotFound()
    {
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsNullForAnyArgs();
        var command = new DeleteGroupCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(GroupErrors.NotFoundById);
        await groupRepository.ReceivedWithAnyArgs(0).DeleteGroupByIdAsync(Arg.Any<GroupId>());
    }
}
