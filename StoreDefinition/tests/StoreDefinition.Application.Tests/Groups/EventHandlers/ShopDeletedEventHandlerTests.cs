using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Events;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Tests.Groups.EventHandlers;
public class ShopDeletedEventHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly ShopDeletedEventHandler handler;

    public ShopDeletedEventHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new ShopDeletedEventHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_RemovesShopIdsFromGroup_WhenGroupsExists()
    {
        var shopId = Guid.NewGuid();
        var group1 = GroupFactory.Create();
        group1.AddShop(shopId);
        var group2 = GroupFactory.Create();
        group2.AddShop(shopId);
        groupRepository.GetGroupsByShopIdAsync(shopId).Returns([group1, group2]);
        var @event = new ShopDeletedDomainEvent(shopId);

        await handler.Handle(@event, default);

        group1.HasShop(shopId).Should().BeFalse();
        group2.HasShop(shopId).Should().BeFalse();
    }
}
