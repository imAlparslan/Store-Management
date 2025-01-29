namespace CatalogManagement.Application.Tests.Products.Handlers;
public class UpdateProductGroupCommandHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly UpdateProductCommandHandler handler;
    public UpdateProductGroupCommandHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new UpdateProductCommandHandler(productRepository);
    }

    [Fact]
    public async Task Handler_ReturnsProduct_WhenDataValid()
    {
        var product = ProductFactory.CreateDefault();
        var command = UpdateProductCommandFactory.CreateValid();
        productRepository.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        productRepository.UpdateAsync(default!).ReturnsForAnyArgs(product);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Value.Should().Be(command.ProductName);
            result.Value!.Code.Value.Should().Be(command.ProductCode);
            result.Value!.Definition.Value.Should().Be(command.ProductDefinition);
        }
    }

    [Fact]
    public async Task Handler_ReturnsProductError_WhenIdNotExists()
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();
        var command = UpdateProductCommandFactory.CreateValid();

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess!.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductError.NotFoundById);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductNameInvalid(string productName)
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithName(productName);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductCodeInvalid(string productCode)
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithCode(productCode);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductDefinitionInvalid(string productDefinition)
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithDefinition(productDefinition);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }
}
