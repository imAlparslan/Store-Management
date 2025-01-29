namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class CreateProductGroupCommandHandlerTests
{
    private readonly IProductGroupRepository productGroupRepository;
    private readonly CreateProductGroupCommandHandler handler;
    public CreateProductGroupCommandHandlerTests()
    {
        productGroupRepository = Substitute.For<IProductGroupRepository>();
        handler = new CreateProductGroupCommandHandler(productGroupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsSuccessResult_WhenDataValid()
    {
        var command = CreateProductGroupCommandFactory.CreateValid();
        var productGroup = ProductGroupFactory.CreateFromCreateCommand(command);
        productGroupRepository.InsertAsync(default!).ReturnsForAnyArgs(productGroup);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Should().Be(productGroup.Name);
            result.Value!.Description.Should().Be(productGroup.Description);
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductGroupNameInvalid(string productGroupName)
    {
        var command = CreateProductGroupCommandFactory.CreateWithName(productGroupName);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductGroupException>();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductGroupDewscriptionInvalid(string productGroupDescription)
    {
        var command = CreateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductGroupException>();
        }
    }
}
