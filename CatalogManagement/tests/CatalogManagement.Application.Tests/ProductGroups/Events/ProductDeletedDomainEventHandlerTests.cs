using CatalogManagement.Application.ProductGroups.Events;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.Tests.ProductGroups.Events;
public class ProductDeletedDomainEventHandlerTests
{

    [Fact]
    public async Task Handler_RemovesProductIdFromProductGroup_WhenProductGroupHaveProduct()
    {
        var productId = Guid.NewGuid();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        var productGroup = ProductGroupFactory.CreateRandom();
        productGroup.AddProduct(productId);
        productGroupRepository.GetProductGroupsByContainigProductAsync(productId).ReturnsForAnyArgs(new List<ProductGroup> { productGroup });
        var handler = new ProductDeletedDomainEventHandler(productGroupRepository);
        var notification = new ProductDeletedDomainEvent(productId);

        await handler.Handle(notification, CancellationToken.None);

        productGroup.ProductIds.Should().NotContain(productId);
    }
}
