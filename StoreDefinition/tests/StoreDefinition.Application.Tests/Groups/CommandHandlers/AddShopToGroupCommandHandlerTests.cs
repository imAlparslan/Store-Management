using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Commands.AddShopToGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Groups.CommandHandlers;
public class AddShopToGroupCommandHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly AddShopToGroupCommandHandler handler;

    public AddShopToGroupCommandHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new AddShopToGroupCommandHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsGroup_WhenShopAddedToGroup()
    {
        var shopId = Guid.NewGuid();
        var group = GroupFactory.Create();
        groupRepository.GetGroupByIdAsync(group.Id).Returns(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);
        var command = new AddShopToGroupCommand(group.Id, shopId);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsNullForAnyArgs();
        var command = new AddShopToGroupCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(GroupErrors.NotFoundById);
    }

    [Fact]
    public async Task Handler_ReturnsShopNotAddedToGroupError_WhenShopNotAdded()
    {
        var shopId = Guid.NewGuid();
        var group = GroupFactory.Create();
        group.AddShop(shopId);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        var command = new AddShopToGroupCommand(group.Id, shopId);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(GroupErrors.ShopNotAddedToGroup);
    }
}
