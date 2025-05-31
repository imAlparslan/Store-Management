using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;

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

        result.IsSuccess.ShouldBeTrue();
        result.Errors.ShouldBeNull();
        result.Value.ShouldNotBeNull();
        result.Value.Name.ShouldBe(productGroup.Name);
        result.Value.Description.ShouldBe(productGroup.Description);
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public async Task Handler_ThrowsException_WhenProductGroupNameInvalid(string productGroupName)
    {
        var command = CreateProductGroupCommandFactory.CreateWithName(productGroupName);

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
        var command = CreateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var action = () => handler.Handle(command, default);

        var exception = await Should.ThrowAsync<ProductGroupException>(action);
        exception.ShouldSatisfyAllConditions(
            x => x.Code.ShouldBe(ProductGroupError.InvalidDescription.Code),
            x => x.Message.ShouldBe(ProductGroupError.InvalidDescription.Description)
        );
    }
}
