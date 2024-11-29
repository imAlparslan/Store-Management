using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.Tests.Common.Factories.ProductGroupFactories;
using NSubstitute;

namespace CatalogManagement.Application.Tests.ProductGroups;
public class GetAllProductGroupQueryHandlerTests
{
    [Fact]
    public async void Handler_ReturnsProductGroupList_WhenProductsExist()
    {
        var productGroups = new List<ProductGroup>() { ProductGroupFactory.CreateRandom(), ProductGroupFactory.CreateRandom() };
        var command = new GetAllProductGroupsQuery();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetAllAsync().Returns(productGroups);
        var handler = new GetAllProductGroupsQueryHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Contain(productGroups);
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async void Handler_ReturnsEmptyList_WhenNoProductGroupsExist()
    {
        var command = new GetAllProductGroupsQuery();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetAllAsync().Returns(Enumerable.Empty<ProductGroup>());
        var handler = new GetAllProductGroupsQueryHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(Enumerable.Empty<ProductGroup>());
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
