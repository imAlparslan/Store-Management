using System.Threading.Tasks;

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

        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.Name.Value.ShouldBe(command.ProductName);
        result.Value.Code.Value.ShouldBe(command.ProductCode);
        result.Value.Definition.Value.ShouldBe(command.ProductDefinition);
    }

    [Fact]
    public async Task Handler_ReturnsProductError_WhenIdNotExists()
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();
        var command = UpdateProductCommandFactory.CreateValid();

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(ProductError.NotFoundById);
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductNameInvalid(string productName)
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithName(productName);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductError.InvalidName.Code),
            x => x.Message.ShouldBe(ProductError.InvalidName.Description)
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductCodeInvalid(string productCode)
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithCode(productCode);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductError.InvalidCode.Code),
            x => x.Message.ShouldBe(ProductError.InvalidCode.Description)
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductDefinitionInvalid(string productDefinition)
    {
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithDefinition(productDefinition);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductError.InvalidDefinition.Code),
            x => x.Message.ShouldBe(ProductError.InvalidDefinition.Description)
        );
    }
}
