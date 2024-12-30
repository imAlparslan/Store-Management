using CatalogManagement.Application.Products.Events;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Events;
public class NewProductAddedToProductGroupDomainEventHandlerTests
{

    [Fact]
    public async Task Handler_AddsNewGroupToProduct()
    {
        var groupId = Guid.NewGuid();
        var product = ProductFactory.CreateDefault();
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetByIdAsync(product.Id, default).ReturnsForAnyArgs(product);
        var domainEvent = new NewProductAddedToProductGroupDomainEvent(groupId, product.Id);
        var handler = new NewProductAddedToProductGroupDomainEventHandler(productRepository);

        await handler.Handle(domainEvent, default);

        product.GroupIds.Should().Contain(groupId);
    }
}
