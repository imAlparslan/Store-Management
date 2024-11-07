using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Application.Products;
using CatalogManagement.Application.Tests.Common.Factories.CommandFactories;
using CatalogManagement.Application.Tests.Common.Factories.ProductFactories;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Exceptions;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatalogManagement.Application.Tests.Products;
public class UpdateProductCommandHandlerTests
{
    [Fact]
    public async void Handler_ReturnsProduct_WhenDataValid()
    {
        var product = ProductFactory.CreateDefault();
        var command = UpdateProductCommandFactory.CreateValid();
        var productRepository = Substitute.For<IProductRepository>();

        productRepository.GetByIdAsync(default!).ReturnsForAnyArgs(product);
        productRepository.UpdateAsync(default!).ReturnsForAnyArgs(product);


        var handler = new UpdateProductCommandHandler(productRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value!.Name.Value.Should().Be(command.ProductName);
            result.Value!.Code.Value.Should().Be(command.ProductCode);
            result.Value!.Definition.Value.Should().Be(command.ProductDefinition);
        }
    }

    [Fact]
    public async void Handler_ReturnsProductError_WhenIdNotExists()
    {
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).ReturnsNull();
        var handler = new UpdateProductCommandHandler(productRepository);
        var command = UpdateProductCommandFactory.CreateValid();

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess!.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors![0].Should().Be(ProductError.NotFoundById);
        }


    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductNameInvalid(string productName)
    {
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetByIdAsync(Arg.Any<ProductId>()).Returns(ProductFactory.CreateDefault());
        var command = UpdateProductCommandFactory.CreateWithName(productName);
        var handler = new UpdateProductCommandHandler(productRepository);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductCodeInvalid(string productCode)
    {
        var command = CreateProductCommandFactory.CreateWithCode(productCode);
        var handler = new CreateProductCommandHandler(default!);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Handler_ThrowsException_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);
        var handler = new CreateProductCommandHandler(default!);

        var result = () => handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.Should().ThrowExactlyAsync<ProductException>();
        }
    }
}
