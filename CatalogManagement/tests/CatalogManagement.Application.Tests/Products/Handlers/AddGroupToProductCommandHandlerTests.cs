using CatalogManagement.Application.Products.Commands.AddGroup;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Handlers;
public class AddGroupToProductCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldReturn_Product_WhenDataValid()
    {
        var product = ProductFactory.CreateDefault();
        var group = ProductGroupFactory.CreateDefault();
        var command = new AddGroupToProductCommand(product.Id, group.Id);
        var repo = Substitute.For<IProductRepository>();
        repo.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        repo.UpdateAsync(default!).ReturnsForAnyArgs(product);
        var handler = new AddGroupToProductCommandHandler(repo);

        var result = await handler.Handle(command, default);

        result.Value!.GroupIds.Should().Contain(group.Id);
        result.Value.GetDomainEvents().Should().HaveCount(1);
        result.Value.GetDomainEvents().Should().ContainItemsAssignableTo<NewGroupAddedToProductDomainEvent>();
    }

    [Fact]
    public async Task Handler_ShouldReturn_NotFoundError_WhenProductNotFound()
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), Guid.NewGuid());
        var repo = Substitute.For<IProductRepository>();
        repo.GetByIdAsync(default!).ReturnsNull();
        var handler = new AddGroupToProductCommandHandler(repo);

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);

    }
}
