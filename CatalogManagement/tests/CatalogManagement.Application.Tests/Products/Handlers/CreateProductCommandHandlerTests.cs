namespace CatalogManagement.Application.Tests.Products.Handlers;
public class CreateProductCommandHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsSuccessResult_WhenDataValid()
    {
        var command = CreateProductCommandFactory.CreateValid();
        var product = ProductFactory.CreateFromCreateCommand(command);
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.InsertAsync(default!).ReturnsForAnyArgs(product);
        var handler = new CreateProductCommandHandler(productRepository);

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductNameInvalid(string productName)
    {
        var command = CreateProductCommandFactory.CreateWithName(productName);
        var handler = new CreateProductCommandHandler(default!);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductCodeInvalid(string productCode)
    {
        var command = CreateProductCommandFactory.CreateWithCode(productCode);
        var handler = new CreateProductCommandHandler(default!);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);
        var handler = new CreateProductCommandHandler(default!);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }
}
