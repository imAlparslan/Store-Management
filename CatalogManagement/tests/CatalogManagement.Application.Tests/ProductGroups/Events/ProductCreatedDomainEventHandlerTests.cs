using CatalogManagement.Application.ProductGroups.Events;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.Tests.ProductGroups.Events;
public class ProductCreatedDomainEventHandlerTests
{

    [Fact]
    public async Task Handler_AddsNewProductToGroups()
    {
        var productGroup = ProductGroupFactory.CreateRandom();
        var product = Product.Create(new("name"), new("code"), new("description"), [productGroup.Id]);
        var @event = new ProductCreatedDomainEvent(product);
        var groupRepository = Substitute.For<IProductGroupRepository>();
        groupRepository.GetByIdsAsync(Arg.Any<IReadOnlyList<Guid>>(), default).ReturnsForAnyArgs(new List<ProductGroup> { productGroup });
        var handler = new ProductCreatedDomainEventHandler(groupRepository);

        await handler.Handle(@event, default);

        productGroup.ProductIds.Should().Contain(product.Id);
    }
}
