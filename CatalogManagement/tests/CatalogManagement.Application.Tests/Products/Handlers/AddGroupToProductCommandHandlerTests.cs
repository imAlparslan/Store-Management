using CatalogManagement.Application.Products.Commands.AddGroup;

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
    }
}
