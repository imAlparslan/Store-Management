namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class GetProductGroupsByIdQueryHandlerTests
{
    [Fact]
    public async void Handler_ReturnsProductGroup_WhenIdExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = new GetProductGroupByIdQuery(productGroup.Id);
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(productGroup);
        var handler = new GetProductGroupByIdQueryHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(productGroup);
            result.Value.Should().BeEquivalentTo(productGroup);
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async void Handler_ReturnsProductGroupError_WhenIdNotExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = new GetProductGroupByIdQuery(productGroup.Id);
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();
        var handler = new GetProductGroupByIdQueryHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().Be(default);
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductGroupError.NotFoundById);
        }
    }
}
