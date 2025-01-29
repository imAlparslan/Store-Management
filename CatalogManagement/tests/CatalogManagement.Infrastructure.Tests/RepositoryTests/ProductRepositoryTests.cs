using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
using CatalogManagement.Infrastructure.Tests.Fixtures;

namespace CatalogManagement.Infrastructure.Tests.RepositoryTests;

public class ProductRepositoryTests : IClassFixture<ProductRepositoryFixture>, IAsyncLifetime
{
    private readonly IProductRepository _productRepository;
    private readonly Func<Task> _dbReset;
    public ProductRepositoryTests(ProductRepositoryFixture productRepositoryFixture)
    {
        _productRepository = productRepositoryFixture.productRepository;
        _dbReset = productRepositoryFixture.ResetDb;
    }

    [Fact]
    public async Task FindById_ReturnsProduct_WhenProductExists()
    {
        var product = ProductFactory.Create();

        var inserted = await _productRepository.InsertAsync(product);
        var result = await _productRepository.GetByIdAsync(product.Id);

        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(product);
            result.Should().BeEquivalentTo(inserted);
        }
    }

    [Fact]
    public async Task DeleteById_ReturnsTrue_WhenProductDeleted()
    {
        var product = ProductFactory.Create();
        _ = await _productRepository.InsertAsync(product);

        var result = await _productRepository.DeleteByIdAsync(product.Id);
        var isExists = await _productRepository.IsExistsAsync(product.Id);

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            isExists.Should().BeFalse();
        }
    }

    [Fact]
    public async Task InsertProduct_ReturnsInsertedProduct_WhenProductInserted()
    {
        var product = ProductFactory.Create();

        var inserted = await _productRepository.InsertAsync(product);
        var isExists = await _productRepository.IsExistsAsync(product.Id);

        using (new AssertionScope())
        {
            isExists.Should().BeTrue();
            inserted.Id.Should().Be(product.Id);
        }
    }

    [Fact]
    public async Task UpdateProduct_ReturnsUpdatedProduct_WhenNameUpdated()
    {
        var product = ProductFactory.Create();
        var oldName = product.Name;
        var newName = ProductNameFactory.Create("updated name");

        var inserted = await _productRepository.InsertAsync(product);
        inserted.ChangeName(newName);
        Product result = await _productRepository.UpdateAsync(inserted);

        using (new AssertionScope())
        {
            result.Name.Should().NotBe(oldName);
            result.Name.Should().Be(newName);
            inserted.Name.Should().Be(result.Name);

        }
    }

    [Fact]
    public async Task UpdateProduct_ReturnsUpdatedProduct_WhenCodeUpdated()
    {
        var product = ProductFactory.Create();
        var oldCode = product.Code;
        var newCode = ProductCodeFactory.Create("updated code");

        var inserted = await _productRepository.InsertAsync(product);
        inserted.ChangeCode(newCode);
        Product result = await _productRepository.UpdateAsync(inserted);

        using (new AssertionScope())
        {
            result.Code.Should().NotBe(oldCode);
            result.Code.Should().Be(newCode);
            inserted.Code.Should().Be(result.Code);

        }
    }

    [Fact]
    public async Task UpdateProduct_ReturnsUpdatedProduct_WhenDefinitionUpdated()
    {
        var product = ProductFactory.Create();
        var oldDefinition = product.Definition;
        var newDefinition = ProductDefinitionFactory.Create("updated definition");

        var inserted = await _productRepository.InsertAsync(product);
        inserted.ChangeDefinition(newDefinition);
        Product result = await _productRepository.UpdateAsync(inserted);

        using (new AssertionScope())
        {
            result.Definition.Should().NotBe(oldDefinition);
            result.Definition.Should().Be(newDefinition);
            inserted.Definition.Should().Be(result.Definition);

        }
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyCollection_WhenNoProductExists()
    {
        var result = await _productRepository.GetAllAsync();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByGroup_Returns_Collection()
    {
        var groupId = Guid.NewGuid();
        var product = ProductFactory.Create();
        product.AddGroup(groupId);
        _ = await _productRepository.InsertAsync(product);
        var result = await _productRepository.GetByGroupAsync(groupId);
        result.Should().HaveCount(1);
    }

    public async Task InitializeAsync() => await _dbReset();

    public Task DisposeAsync() => Task.CompletedTask;
}