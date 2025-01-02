using CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Handlers;
public class RemoveGroupFromProductCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldReturn_Product_WhenDataValid()
    {
        var product = ProductFactory.CreateDefault();
        var group = ProductGroupFactory.CreateDefault();
        product.AddGroup(group.Id);
        var command = new RemoveGroupFromProductCommand(group.Id, product.Id);
        var repo = Substitute.For<IProductRepository>();
        repo.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        repo.UpdateAsync(default!).ReturnsForAnyArgs(product);
        var handler = new RemoveGroupFromProductCommandHandler(repo);

        var result = await handler.Handle(command, default);

        result.Value!.GroupIds.Should().NotContain(group.Id);
        result.Value.GetDomainEvents().Should().HaveCount(1);
        result.Value.GetDomainEvents().Should().ContainItemsAssignableTo<GroupRemovedFromProductDomainEvent>();
    }

    [Fact]
    public async Task Handler_ShouldReturn_NotFoundError_WhenProductNotFound()
    {
        var command = new RemoveGroupFromProductCommand(Guid.NewGuid(), Guid.NewGuid());
        var repo = Substitute.For<IProductRepository>();
        repo.GetByIdAsync(default!).ReturnsNull();
        var handler = new RemoveGroupFromProductCommandHandler(repo);

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupNotDeletedError_WhenProductGroupNotRemoved()
    {
        var product = ProductFactory.CreateDefault();
        var command = new RemoveGroupFromProductCommand(Guid.NewGuid(), product.Id);
        var repo = Substitute.For<IProductRepository>();
        repo.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        var handler = new RemoveGroupFromProductCommandHandler(repo);

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(ProductError.ProductGroupNotDeletedFromProduct);
    }
}
