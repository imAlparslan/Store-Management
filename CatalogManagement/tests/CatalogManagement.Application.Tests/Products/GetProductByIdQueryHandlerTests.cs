using CatalogManagement.Application.Common;
using CatalogManagement.Application.Products;
using CatalogManagement.Application.Tests.Common.Factories.ProductFactories;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatalogManagement.Application.Tests.Products;
public class GetProductByIdQueryHandlerTests
{
    [Fact]
    public async void Handler_ReturnsProduct_WhenIdExists()
    {
        var product = ProductFactory.CreateDefault();
        var command = new GetProductByIdQuery(product.Id);
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(product);
        var handler = new GetProductByIdQueryHandler(productRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(product);
            result.Value.Should().BeEquivalentTo(product);
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async void Handler_ReturnsProductError_WhenIdNotExists()
    {
        var product = ProductFactory.CreateDefault();
        var command = new GetProductByIdQuery(product.Id);
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();
        var handler = new GetProductByIdQueryHandler(productRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().Be(default);
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductError.NotFoundById);
        }
    }
}
