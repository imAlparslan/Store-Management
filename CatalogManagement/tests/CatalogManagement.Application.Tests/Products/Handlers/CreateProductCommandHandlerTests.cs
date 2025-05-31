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


        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.Name.ShouldBe(product.Name);
        result.Value.Code.ShouldBe(product.Code);
        result.Value.Definition.ShouldBe(product.Definition);
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductNameInvalid(string productName)
    {
        var command = CreateProductCommandFactory.CreateWithName(productName);

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
        var command = CreateProductCommandFactory.CreateWithCode(productCode);

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
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductError.InvalidDefinition.Code),
            x => x.Message.ShouldBe(ProductError.InvalidDefinition.Description)
        );
    }
}
