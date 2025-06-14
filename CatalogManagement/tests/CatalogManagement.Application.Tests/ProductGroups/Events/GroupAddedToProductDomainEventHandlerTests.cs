﻿using CatalogManagement.Application.ProductGroups.Events;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.Tests.ProductGroups.Events;
public class GroupAddedToProductDomainEventHandlerTests
{
    [Fact]
    public async Task Handler_AddsProductIdToProductGroup_WhenProductGroupExists()
    {
        var productId = Guid.NewGuid();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        var productGroup = ProductGroupFactory.CreateRandom();
        productGroupRepository.GetByIdAsync(productGroup.Id, default).ReturnsForAnyArgs(productGroup);
        var handler = new GroupAddedToProductDomainEventHandler(productGroupRepository);
        var notification = new NewGroupAddedToProductDomainEvent(productGroup.Id, productId);

        await handler.Handle(notification, CancellationToken.None);

        productGroup.ProductIds.ShouldContain(productId);
    }
}