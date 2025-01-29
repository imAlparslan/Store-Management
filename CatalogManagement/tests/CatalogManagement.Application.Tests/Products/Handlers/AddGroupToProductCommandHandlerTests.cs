using CatalogManagement.Application.Products.Commands.AddGroup;
using CatalogManagement.Domain.ProductAggregate.Events;

namespace CatalogManagement.Application.Tests.Products.Handlers;
public class AddGroupToProductCommandHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly AddGroupToProductCommandHandler handler;
    public AddGroupToProductCommandHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new AddGroupToProductCommandHandler(productRepository);
    }
    [Fact]
    public async Task Handler_ShouldReturn_Product_WhenDataValid()
    {
        var product = ProductFactory.CreateDefault();
        var group = ProductGroupFactory.CreateDefault();
        var command = new AddGroupToProductCommand(product.Id, group.Id);
        productRepository.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        productRepository.UpdateAsync(default!).ReturnsForAnyArgs(product);

        var result = await handler.Handle(command, default);

        result.Value!.GroupIds.Should().Contain(group.Id);
        result.Value.GetDomainEvents().Should().HaveCount(1);
        result.Value.GetDomainEvents().Should().ContainItemsAssignableTo<NewGroupAddedToProductDomainEvent>();
    }

    [Fact]
    public async Task Handler_ShouldReturn_NotFoundError_WhenProductNotFound()
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), Guid.NewGuid());
        productRepository.GetByIdAsync(default!).ReturnsNull();

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(ProductError.NotFoundById);

    }

    [Fact]
    public async Task Handler_ReturnsProductGroupNotAddedToProductError_WhenProductGroupNotAdded()
    {
        var productGroupId = Guid.NewGuid();
        var product = ProductFactory.CreateDefault();
        product.AddGroup(productGroupId);
        var command = new AddGroupToProductCommand(product.Id, productGroupId);
        productRepository.GetByIdAsync(default!).ReturnsForAnyArgs(product);

        var result = await handler.Handle(command, default);

        result.Errors.Should().HaveCount(1);
        result.Errors.Should().Contain(ProductError.ProductGroupNotAddedToProduct);
    }
}
