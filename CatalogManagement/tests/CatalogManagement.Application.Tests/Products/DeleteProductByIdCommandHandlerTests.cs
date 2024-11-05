using CatalogManagement.Application.Common;
using CatalogManagement.Application.Products;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;

namespace CatalogManagement.Application.Tests.Products;
public class DeleteProductByIdCommandHandlerTests
{

    [Fact]
    public async void Handler_ReturnsTrue_WhenIdExists()
    {
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.DeleteByIdAsync(Arg.Any<ProductId>()).Returns(true);
        var handler = new DeleteProductByIdCommandHandler(productRepository);
        var command = new DeleteProductByIdCommand(Guid.NewGuid());

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
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.DeleteByIdAsync(Arg.Any<ProductId>()).Returns(false);
        var handler = new DeleteProductByIdCommandHandler(productRepository);
        var command = new DeleteProductByIdCommand(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Value.Should().BeFalse();
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductError.NotDeleted);
        }
    }
}
