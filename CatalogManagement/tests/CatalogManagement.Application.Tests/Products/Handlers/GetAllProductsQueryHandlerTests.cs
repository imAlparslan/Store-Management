using CatalogManagement.Domain.ProductAggregate;

namespace CatalogManagement.Application.Tests.Products.Handlers;

public class GetAllProductGroupQueryHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly GetAllProductsQueryHandler handler;
    public GetAllProductGroupQueryHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new GetAllProductsQueryHandler(productRepository);
    }
    [Fact]
    public async Task Handler_ReturnsProductList_WhenProductsExist()
    {
        var products = new List<Product>() { ProductFactory.CreateRandom(), ProductFactory.CreateRandom() };
        var command = new GetAllProductsQuery();
        productRepository.GetAllAsync().Returns(products);

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeSubsetOf(products);
        result.Errors.ShouldBeNull();

    }

    [Fact]
    public async Task Handler_ReturnsEmptyList_WhenNoProductsExist()
    {
        var command = new GetAllProductsQuery();
        productRepository.GetAllAsync().Returns(Enumerable.Empty<Product>());

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
        result.Errors.ShouldBeNull();

    }
}
