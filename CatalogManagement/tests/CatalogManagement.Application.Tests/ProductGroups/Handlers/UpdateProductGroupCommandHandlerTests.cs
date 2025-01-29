namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class UpdateProductGroupCommandHandlerTests
{
    private readonly IProductGroupRepository productGroupRepository;
    private readonly UpdateProductGroupCommandHandler handler;
    public UpdateProductGroupCommandHandlerTests()
    {
        productGroupRepository = Substitute.For<IProductGroupRepository>();
        handler = new UpdateProductGroupCommandHandler(productGroupRepository);
    }
    [Fact]
    public async Task Handler_ReturnsProductGroup_WhenDataValid()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = UpdateProductGroupCommandFactory.CreateValid();
        productGroupRepository.GetByIdAsync(default!).ReturnsForAnyArgs(productGroup);
        productGroupRepository.UpdateAsync(default!).ReturnsForAnyArgs(productGroup);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Value.Should().Be(command.Name);
            result.Value!.Description.Value.Should().Be(command.Description);
        }
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupError_WhenIdNotExists()
    {
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();
        var command = UpdateProductGroupCommandFactory.CreateValid();

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess!.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors.Should().Contain(ProductGroupError.NotFoundById);
        }


    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductGroupNameInvalid(string productGroupName)
    {
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(ProductGroupFactory.CreateDefault());
        var command = UpdateProductGroupCommandFactory.CreateWithName(productGroupName);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductGroupException>();
        }
    }


    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Handler_ThrowsException_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(ProductGroupFactory.CreateDefault());
        var command = UpdateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductGroupException>();
        }
    }
}
