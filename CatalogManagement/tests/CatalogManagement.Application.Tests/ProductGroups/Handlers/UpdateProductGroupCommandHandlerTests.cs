using System.Threading.Tasks;

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

        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.Name.Value.ShouldBe(command.Name);
        result.Value.Description.Value.ShouldBe(command.Description);
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupError_WhenIdNotExists()
    {
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();
        var command = UpdateProductGroupCommandFactory.CreateValid();

        var result = await handler.Handle(command, default);

        result.IsSuccess!.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(ProductGroupError.NotFoundById);
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductGroupNameInvalid(string productGroupName)
    {
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(ProductGroupFactory.CreateDefault());
        var command = UpdateProductGroupCommandFactory.CreateWithName(productGroupName);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductGroupException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductGroupError.InvalidName.Code),
            x => x.Message.ShouldBe(ProductGroupError.InvalidName.Description)
        );
    }


    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(ProductGroupFactory.CreateDefault());
        var command = UpdateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductGroupException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductGroupError.InvalidDescription.Code),
            x => x.Message.ShouldBe(ProductGroupError.InvalidDescription.Description)
        );
    }
}
