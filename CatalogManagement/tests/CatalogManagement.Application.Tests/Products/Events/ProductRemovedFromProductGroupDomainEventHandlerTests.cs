using CatalogManagement.Application.Products.Events;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Events;
public class ProductRemovedFromProductGroupDomainEventHandlerTests
{
    [Fact]
    public async Task Handler_RemovesProductGroupFromProduct()
    {
        var productGroupId = Guid.NewGuid();
        var product = ProductFactory.CreateDefault();
        product.AddGroup(productGroupId);
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetByIdAsync(product.Id, default).ReturnsForAnyArgs(product);
        var handler = new ProductRemovedFromProductGroupDomainEventHandler(productRepository);
        var @event = new ProductRemovedFromProductGroupDomainEvent(productGroupId, product.Id);

        await handler.Handle(@event, CancellationToken.None);

        product.GroupIds.Should().NotContain(productGroupId);

    }

}
