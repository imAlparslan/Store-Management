namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;

public class DeleteProductGroupByIdCommandHandlerTests
{
    private readonly IProductGroupRepository productGroupRepository;
    private readonly DeleteProductGroupByIdCommandHandler handler;
    public DeleteProductGroupByIdCommandHandlerTests()
    {
        productGroupRepository = Substitute.For<IProductGroupRepository>();
        handler = new DeleteProductGroupByIdCommandHandler(productGroupRepository);
    }
    [Fact]
    public async Task Handler_ReturnsTrue_WhenIdExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        productGroupRepository.GetByIdAsync(default!).ReturnsForAnyArgs(productGroup);
        productGroupRepository.DeleteByIdAsync(default!).ReturnsForAnyArgs(true);
        var command = new DeleteProductGroupByIdCommand(productGroup.Id);

        var action = await handler.Handle(command, default);

        action.Value.ShouldBeTrue();
        action.IsSuccess.ShouldBeTrue();
        action.Errors.ShouldBeNull();
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundById_WhenIdNotExists()
    {
        productGroupRepository.DeleteByIdAsync(Arg.Any<ProductGroupId>()).Returns(false);
        var command = new DeleteProductGroupByIdCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        result.Value.ShouldBeFalse();
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(ProductGroupError.NotFoundById);
    }
}
