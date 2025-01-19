using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Events;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Tests.Groups.EventHandlers;
public class GroupRemovedFromShopEventHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly GroupRemovedFromShopEventHandler handler;

    public GroupRemovedFromShopEventHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new GroupRemovedFromShopEventHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_RemovesShopFromGroup_WhenGroupExists()
    {
        var shopId = Guid.NewGuid();
        var group = GroupFactory.Create();
        group.AddShop(shopId);
        groupRepository.GetGroupByIdAsync(group.Id).Returns(group);
        var @event = new GroupRemovedFromShopDomainEvent(shopId, group.Id);

        await handler.Handle(@event, default);

        group.HasShop(shopId).Should().BeFalse();
    }
}
