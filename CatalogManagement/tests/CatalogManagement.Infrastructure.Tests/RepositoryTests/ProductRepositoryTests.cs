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

        result.ShouldBeEquivalentTo(product);
        result.ShouldBeEquivalentTo(inserted);
    }

    [Fact]
    public async Task DeleteById_ReturnsTrue_WhenProductDeleted()
    {
        var product = ProductFactory.Create();
        _ = await _productRepository.InsertAsync(product);

        var result = await _productRepository.DeleteByIdAsync(product.Id);
        var isExists = await _productRepository.IsExistsAsync(product.Id);

        result.ShouldBeTrue();
        isExists.ShouldBeFalse();
    }

    [Fact]
    public async Task InsertProduct_ReturnsInsertedProduct_WhenProductInserted()
    {
        var product = ProductFactory.Create();

        var inserted = await _productRepository.InsertAsync(product);
        var isExists = await _productRepository.IsExistsAsync(product.Id);

        isExists.ShouldBeTrue();
        inserted.Id.ShouldBe(product.Id);
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

        result.Name.ShouldNotBe(oldName);
        result.Name.ShouldBe(newName);
        inserted.Name.ShouldBe(result.Name);

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

        result.Code.ShouldNotBe(oldCode);
        result.Code.ShouldBe(newCode);
        inserted.Code.ShouldBe(result.Code);

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

        result.Definition.ShouldNotBe(oldDefinition);
        result.Definition.ShouldBe(newDefinition);
        inserted.Definition.ShouldBe(result.Definition);

    }

    [Fact]
    public async Task GetAll_ReturnsEmptyCollection_WhenNoProductExists()
    {
        var result = await _productRepository.GetAllAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetByGroup_Returns_Collection()
    {
        var groupId = Guid.NewGuid();
        var product = ProductFactory.Create();
        product.AddGroup(groupId);
        _ = await _productRepository.InsertAsync(product);

        var result = await _productRepository.GetByGroupAsync(groupId);

        result.ShouldHaveSingleItem();
    }

    public async Task InitializeAsync() => await _dbReset();

    public Task DisposeAsync() => Task.CompletedTask;
}