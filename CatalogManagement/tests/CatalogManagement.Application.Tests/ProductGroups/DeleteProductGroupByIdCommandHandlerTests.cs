using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.Products;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using NSubstitute;

namespace CatalogManagement.Application.Tests.ProductsGroups;
public class DeleteProductGroupByIdCommandHandlerTests
{

    [Fact]
    public async void Handler_ReturnsTrue_WhenIdExists()
    {
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.DeleteByIdAsync(Arg.Any<ProductGroupId>()).Returns(true);
        var handler = new DeleteProductGroupByIdCommandHandler(productGroupRepository);
        var command = new DeleteProductGroupByIdCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Value.Should().BeTrue();
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async void Handler_ReturnsNotDeleted_WhenIdNotExists()
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
            result.Errors![0].Should().Be(ProductGroupError.NotDeleted);
        }
    }
}
