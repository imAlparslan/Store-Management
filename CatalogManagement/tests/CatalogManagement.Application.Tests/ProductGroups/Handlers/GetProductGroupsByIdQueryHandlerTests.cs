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

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(productGroup);
        result.Value.ShouldBeEquivalentTo(productGroup);
        result.Errors.ShouldBeNull();
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupError_WhenIdNotExists()
    {
        var productGroup = ProductGroupFactory.CreateDefault();
        var command = new GetProductGroupByIdQuery(productGroup.Id);
        productGroupRepository.GetByIdAsync(Arg.Any<ProductGroupId>()).ReturnsNull();

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBe(default);
        result.Errors.ShouldNotBeEmpty();
        result.Errors.ShouldContain(ProductGroupError.NotFoundById);
    }
}