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

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(product);
            result.Value.Should().BeEquivalentTo(product);
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Handler_ReturnsProductError_WhenIdNotExists()
    {
        var product = ProductFactory.CreateDefault();
        var command = new GetProductByIdQuery(product.Id);
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().Be(default);
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductError.NotFoundById);
        }
    }
}
