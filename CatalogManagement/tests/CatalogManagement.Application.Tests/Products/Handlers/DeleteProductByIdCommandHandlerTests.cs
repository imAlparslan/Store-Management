namespace CatalogManagement.Application.Tests.Products.Handlers;
public class DeleteProductGroupByIdCommandHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly DeleteProductByIdCommandHandler handler;
    public DeleteProductGroupByIdCommandHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new DeleteProductByIdCommandHandler(productRepository);
    }
    [Fact]
    public async Task Handler_ReturnsTrue_WhenIdExists()
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        productRepository.DeleteByIdAsync(Arg.Any<ProductId>()).Returns(true);
        var command = new DeleteProductByIdCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Value.Should().BeTrue();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Handler_ReturnsNotDeleted_WhenIdNotExists()
    {
        productRepository.DeleteByIdAsync(Arg.Any<ProductId>()).Returns(false);
        var command = new DeleteProductByIdCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Value.Should().BeFalse();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductError.NotFoundById);
        }
    }
}
