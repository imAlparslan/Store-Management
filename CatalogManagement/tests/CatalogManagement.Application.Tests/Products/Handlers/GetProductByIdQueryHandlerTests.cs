namespace CatalogManagement.Application.Tests.Products.Handlers;

public class GetProductGroupsByIdQueryHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly GetProductByIdQueryHandler handler;
    public GetProductGroupsByIdQueryHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new GetProductByIdQueryHandler(productRepository);
    }

    [Fact]
    public async Task Handler_ReturnsProduct_WhenIdExists()
    {
        var product = ProductFactory.CreateDefault();
        var command = new GetProductByIdQuery(product.Id);
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(product);

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(product);
        result.Value.ShouldBeEquivalentTo(product);
        result.Errors.ShouldBeNull();
    }

    [Fact]
    public async Task Handler_ReturnsProductError_WhenIdNotExists()
    {
        var product = ProductFactory.CreateDefault();
        var command = new GetProductByIdQuery(product.Id);
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBe(default);
        result.Errors.ShouldNotBeEmpty();
        result.Errors[0].ShouldBe(ProductError.NotFoundById);
    }
}
