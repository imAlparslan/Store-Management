namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class GetProductGroupsByIdQueryHandlerTests
{
    private readonly IProductGroupRepository productGroupRepository;
    private readonly GetProductGroupByIdQueryHandler handler;
    public GetProductGroupsByIdQueryHandlerTests()
    {
        productGroupRepository = Substitute.For<IProductGroupRepository>();
        handler = new GetProductGroupByIdQueryHandler(productGroupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsProductGroup_WhenIdExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = new GetProductGroupByIdQuery(productGroup.Id);
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).Returns(productGroup);

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
    public async Task Handler_ReturnsProductGroupError_WhenIdNotExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = new GetProductGroupByIdQuery(productGroup.Id);
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();

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