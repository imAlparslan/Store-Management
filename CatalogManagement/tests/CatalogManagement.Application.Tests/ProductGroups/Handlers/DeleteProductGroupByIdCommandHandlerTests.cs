namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class DeleteProductGroupByIdCommandHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsTrue_WhenIdExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(default!).ReturnsForAnyArgs(productGroup);
        productGroupRepository.DeleteByIdAsync(default!).ReturnsForAnyArgs(true);
        var handler = new DeleteProductGroupByIdCommandHandler(productGroupRepository);
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
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.DeleteByIdAsync(Arg.Any<ProductGroupId>()).Returns(false);
        var handler = new DeleteProductGroupByIdCommandHandler(productGroupRepository);
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
