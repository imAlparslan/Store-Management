using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Events;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.GroupAggregateRoot.Events;
using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Tests.Shops.EventHandlers;
public class GroupDeletedDomainEventHandlerTests
{
    private readonly IShopRepository shopRepository;
    private readonly GroupDeletedDomainEventHandler handler;

    public GroupDeletedDomainEventHandlerTests()
    {
        shopRepository = Substitute.For<IShopRepository>();
        handler = new GroupDeletedDomainEventHandler(shopRepository);
    }

    [Fact]
    public async Task Handler_RemovesGroupFromShops()
    {
        var groupId = Guid.NewGuid();
        var shop1 = ShopFactory.CreateValid();
        shop1.AddGroup(groupId);
        var shop2 = ShopFactory.CreateValid();
        shop2.AddGroup(groupId);
        shopRepository.GetShopsByGroupIdAsync(groupId).Returns([shop1, shop2]);
        var @event = new GroupDeletedDomainEvent(groupId);

        await handler.Handle(@event, default);

        shop1.HasGroup(groupId).Should().BeFalse();
        await shopRepository.ReceivedWithAnyArgs(2).UpdateShopAsync(Arg.Any<Shop>());

    }
}
