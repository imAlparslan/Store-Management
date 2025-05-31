using CatalogManagement.Application.Products.Events;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Events;
public class ProductGroupDeletedDomainEventHandlerTests
{
    [Fact]
    public async Task Handle_RemovesGroupFromProducts()
    {
        var productGroupId = Guid.NewGuid();
        var productRepositoryMock = Substitute.For<IProductRepository>();
        var productGroupDeletedDomainEventHandler = new ProductGroupDeletedDomainEventHandler(productRepositoryMock);
        var product = ProductFactory.CreateDefault();
        product.AddGroup(productGroupId);
        productRepositoryMock.GetByGroupAsync(productGroupId, Arg.Any<CancellationToken>()).Returns(new List<Product> { product });

        await productGroupDeletedDomainEventHandler.Handle(new ProductGroupDeletedDomainEvent(productGroupId), CancellationToken.None);

        product.GroupIds.ShouldNotContain(productGroupId);
    }
}
