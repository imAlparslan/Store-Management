using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Commands.RemoveShopFromGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Groups.CommandHandlers;
public sealed class RemoveShopFromGroupCommandHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly RemoveShopFromGroupCommandHandler handler;

    public RemoveShopFromGroupCommandHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new RemoveShopFromGroupCommandHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsGroup_WhenShopRemoved()
    {
        var shopId = Guid.NewGuid();
        var group = GroupFactory.Create();
        group.AddShop(shopId);
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        groupRepository.UpdateGroupAsync(Arg.Any<Group>()).ReturnsForAnyArgs(group);
        var command = new RemoveShopFromGroupCommand(group.Id, shopId);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value!.HasShop(shopId).Should().BeFalse();
        result.Value!.GetDomainEvents().Should().HaveCount(1);
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsNullForAnyArgs();
        var command = new RemoveShopFromGroupCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(GroupErrors.NotFoundById);
    }

    [Fact]
    public async Task Handler_ReturnsShopNotRemovedFromGroupError_WhenShopNotRemoved()
    {
        var group = GroupFactory.Create();
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsForAnyArgs(group);
        var command = new RemoveShopFromGroupCommand(group.Id, Guid.NewGuid());

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(GroupErrors.ShopNotRemovedFromGroup);
    }
}