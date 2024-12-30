using CatalogManagement.Application.ProductGroups.Events;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.Tests.ProductGroups.Events;
public class GroupRemovedFromProductDomainEventHandlerTests
{

    [Fact]
    public async Task Handler_RemovesProductIdFromGroup_WhenProductIdExists()
    {
        var productId = Guid.NewGuid();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        var productGroup = ProductGroupFactory.CreateRandom();
        productGroup.AddProduct(productId);
        productGroupRepository.GetByIdAsync(productGroup.Id, default).ReturnsForAnyArgs(productGroup);
        var handler = new GroupRemovedFromProductDomainEventHandler(productGroupRepository);
        var notification = new GroupRemovedFromProductDomainEvent(productGroup.Id, productId);
        
        await handler.Handle(notification, CancellationToken.None);
        
        productGroup.ProductIds.Should().NotContain(productId);

    }
}
