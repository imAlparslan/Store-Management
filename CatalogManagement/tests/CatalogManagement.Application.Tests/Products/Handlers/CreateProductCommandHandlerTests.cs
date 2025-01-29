namespace CatalogManagement.Application.Tests.Products.Handlers;
public class CreateProductCommandHandlerTests
{
    private readonly IProductRepository productRepository;
    private readonly CreateProductCommandHandler handler;
    public CreateProductCommandHandlerTests()
    {
        productRepository = Substitute.For<IProductRepository>();
        handler = new CreateProductCommandHandler(productRepository);
    }

    [Fact]
    public async Task Handler_ReturnsSuccessResult_WhenDataValid()
    {
        var command = CreateProductCommandFactory.CreateValid();
        var product = ProductFactory.CreateFromCreateCommand(command);
        productRepository.InsertAsync(default!).ReturnsForAnyArgs(product);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Should().Be(product.Name);
            result.Value!.Code.Should().Be(product.Code);
            result.Value!.Definition.Should().Be(product.Definition);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductNameInvalid(string productName)
    {
        var command = CreateProductCommandFactory.CreateWithName(productName);

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
        var command = CreateProductCommandFactory.CreateWithCode(productCode);

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
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }
}
