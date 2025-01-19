using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Events;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.GroupAggregateRoot.Events;
using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Tests.Shops.EventHandlers;
public class ShopAddedToGroupDomainEventHandlerTests
{
    private readonly IShopRepository shopRepository;
    private readonly ShopAddedToGroupDomainEventHandler handler;
    public ShopAddedToGroupDomainEventHandlerTests()
    {
        shopRepository = Substitute.For<IShopRepository>();
        handler = new ShopAddedToGroupDomainEventHandler(shopRepository);
    }

    [Fact]
    public async Task Handler_AddsGroupToShop_WhenShopExists()
    {
        var groupId = Guid.NewGuid();
        var shop = ShopFactory.CreateValid();
        var @event = new ShopAddedToGroupDomainEvent(groupId,shop.Id);
        shopRepository.GetShopByIdAsync(shop.Id).Returns(shop);

        await handler.Handle(@event,default);

        shop.HasGroup(groupId).Should().BeTrue();
        await shopRepository.ReceivedWithAnyArgs(1).UpdateShopAsync(Arg.Any<Shop>());

    }
}
