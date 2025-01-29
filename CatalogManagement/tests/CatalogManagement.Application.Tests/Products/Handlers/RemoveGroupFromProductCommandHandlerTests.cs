using CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Handlers;
public class RemoveGroupFromProductCommandHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly RemoveGroupFromProductCommandHandler handler;
    public RemoveGroupFromProductCommandHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new RemoveGroupFromProductCommandHandler(productRepository);
    }

    [Fact]
    public async Task Handler_ShouldReturn_Product_WhenDataValid()
    {
        var product = ProductFactory.CreateDefault();
        var group = ProductGroupFactory.CreateDefault();
        product.AddGroup(group.Id);
        var command = new RemoveGroupFromProductCommand(group.Id, product.Id);
        productRepository.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        productRepository.UpdateAsync(default!).ReturnsForAnyArgs(product);

        var result = await handler.Handle(command, default);

        result.Value!.GroupIds.Should().NotContain(group.Id);
        result.Value.GetDomainEvents().Should()
            .ContainEquivalentOf(new GroupRemovedFromProductDomainEvent(group.Id, product.Id));
    }

    [Fact]
    public async Task Handler_ShouldReturn_NotFoundError_WhenProductNotFound()
    {
        var command = new RemoveGroupFromProductCommand(Guid.NewGuid(), Guid.NewGuid());
        productRepository.GetByIdAsync(default!).ReturnsNull();

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupNotDeletedError_WhenProductGroupNotRemoved()
    {
        var product = ProductFactory.CreateDefault();
        var command = new RemoveGroupFromProductCommand(Guid.NewGuid(), product.Id);
        productRepository.GetByIdAsync(default!).ReturnsForAnyArgs(product);

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(ProductError.ProductGroupNotDeletedFromProduct);
    }
}