using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Events;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.GroupAggregateRoot.Events;
using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Tests.Shops.EventHandlers;
public class ShopRemovedFromGroupDomainEventHandlerTests
{
    private readonly IShopRepository shopRepository;
    private readonly ShopRemovedFromGroupDomainEventHandler handler;

    public ShopRemovedFromGroupDomainEventHandlerTests()
    {
        shopRepository = Substitute.For<IShopRepository>();
        handler = new ShopRemovedFromGroupDomainEventHandler(shopRepository);
    }

    [Fact]
    public async Task Handler_RemovesGroupFromShop_WhenShopExists()
    {
        var groupId = Guid.NewGuid();
        var shop = ShopFactory.CreateValid();
        shop.AddGroup(groupId);
        var @event = new ShopRemovedFromGroupDomainEvent(groupId, shop.Id);
        shopRepository.GetShopByIdAsync(shop.Id).Returns(shop);

        await handler.Handle(@event, default);

        shop.HasGroup(groupId).Should().BeFalse();
        await shopRepository.ReceivedWithAnyArgs(1).UpdateShopAsync(Arg.Any<Shop>());

    }
}
