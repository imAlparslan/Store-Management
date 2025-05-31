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

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.GroupIds.ShouldContain(group.Id);
        result.Value.GetDomainEvents().ShouldHaveSingleItem();
        result.Value.GetDomainEvents()
            .ShouldContain(x => x.GetType() == typeof(NewGroupAddedToProductDomainEvent));
    }

    [Fact]
    public async Task Handler_ShouldReturn_NotFoundError_WhenProductNotFound()
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), Guid.NewGuid());
        productRepository.GetByIdAsync(default!).ReturnsNull();

        var result = await handler.Handle(command, default);

        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(ProductError.NotFoundById);
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

        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(ProductError.ProductGroupNotAddedToProduct);
    }
}
