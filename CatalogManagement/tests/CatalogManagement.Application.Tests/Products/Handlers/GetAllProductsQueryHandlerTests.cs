using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Tests.Products.Handlers;
public class GetAllProductGroupQueryHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsProductList_WhenProductsExist()
    {
        var products = new List<Product>() { ProductFactory.CreateRandom(), ProductFactory.CreateRandom() };
        var command = new GetAllProductsQuery();
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetAllAsync().Returns(products);
        var handler = new GetAllProductsQueryHandler(productRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Contain(products);
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Handler_ReturnsEmptyList_WhenNoProductsExist()
    {
        var command = new GetAllProductsQuery();
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetAllAsync().Returns(Enumerable.Empty<Product>());
        var handler = new GetAllProductsQueryHandler(productRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(Enumerable.Empty<Product>());
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
