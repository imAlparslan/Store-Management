using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Events;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.ShopAggregateRoot.Events;

namespace StoreDefinition.Application.Tests.Groups.EventHandlers;
public class GroupAddedToShopEventHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly GroupAddedToShopEventHandler handler;

    public GroupAddedToShopEventHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new GroupAddedToShopEventHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_AddsShopToGroup_WhenGroupExists()
    {
        var shopId = Guid.NewGuid();
        var group = GroupFactory.Create();
        groupRepository.GetGroupByIdAsync(group.Id).Returns(group);
        var @event = new GroupAddedToShopDomainEvent(shopId, group.Id);

        await handler.Handle(@event, default);

        group.HasShop(shopId).ShouldBeTrue();
    }
}
