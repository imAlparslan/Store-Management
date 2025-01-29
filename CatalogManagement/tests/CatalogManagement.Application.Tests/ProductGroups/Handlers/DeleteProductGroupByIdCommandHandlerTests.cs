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

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Value.Should().BeTrue();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundById_WhenIdNotExists()
    {
        productGroupRepository.DeleteByIdAsync(Arg.Any<ProductGroupId>()).Returns(false);
        var command = new DeleteProductGroupByIdCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Value.Should().BeFalse();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductGroupError.NotFoundById);
        }
    }
}
