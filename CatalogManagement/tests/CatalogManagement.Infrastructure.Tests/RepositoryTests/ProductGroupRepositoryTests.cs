using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
using CatalogManagement.Infrastructure.Tests.Fixtures;

namespace CatalogManagement.Infrastructure.Tests.RepositoryTests;

public class ProductGroupRepositoryTests : IClassFixture<ProductGroupRepositoryFixture>, IAsyncLifetime
{
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly Func<Task> _dbReset;
    public ProductGroupRepositoryTests(ProductGroupRepositoryFixture productGroupRepositoryFixture)
    {
        _productGroupRepository = productGroupRepositoryFixture.productGroupRepository;

        _dbReset = productGroupRepositoryFixture.ResetDb;
    }

    [Fact]
    public async Task FindById_Returns_CorrectProductGroup()
    {
        var productGroup = ProductGroupFactory.Create();

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        var result = await _productGroupRepository.GetByIdAsync(productGroup.Id);

        result.ShouldBeEquivalentTo(productGroup);
        result.ShouldBeEquivalentTo(inserted);
    }

    [Fact]
    public async Task DeleteById_DeletesAndReturnTrue_WhenIdExists()
    {
        var productGroup = ProductGroupFactory.Create();
        _ = await _productGroupRepository.InsertAsync(productGroup);
        var result = await _productGroupRepository.DeleteByIdAsync(productGroup.Id);
        var isExists = await _productGroupRepository.IsExistsAsync(productGroup.Id);

        result.ShouldBeTrue();
        isExists.ShouldBeFalse();
    }

    [Fact]
    public async Task InsertProduct_Returns_InsertedProductGroup()
    {
        var productGroup = ProductGroupFactory.Create();

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        var isExists = await _productGroupRepository.IsExistsAsync(productGroup.Id);

        isExists.ShouldBeTrue();
        inserted.Id.ShouldBe(productGroup.Id);
    }

    [Fact]
    public async Task UpdateProductGroup_ReturnsUpdatedGroup_WhenGroupNameUpdated()
    {
        var productGroup = ProductGroupFactory.Create();
        var oldName = productGroup.Name;
        var newName = ProductGroupNameFactory.Create("updated name");

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        inserted.ChangeName(newName);
        ProductGroup result = await _productGroupRepository.UpdateAsync(inserted);

        result.Name.ShouldNotBe(oldName);
        result.Name.ShouldBe(newName);
        inserted.Name.ShouldBe(result.Name);

    }

    [Fact]
    public async Task UpdateProductGroup_ReturnsUpdatedGroup_WhenGroupDefinitionUpdated()
    {
        var productGroup = ProductGroupFactory.Create();
        var oldDescription = productGroup.Description;
        var newDefinition = ProductGroupDescriptionFactory.Create("updated definition");

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        inserted.ChangeDescription(newDefinition);
        ProductGroup result = await _productGroupRepository.UpdateAsync(inserted);

        result.Description.ShouldNotBe(oldDescription);
        result.Description.ShouldBe(newDefinition);
        inserted.Description.ShouldBe(result.Description);

    }

    [Fact]
    public async Task GetAll_ReturnsEmptyCollection_WhenNoProductGroupExists()
    {
        var result = await _productGroupRepository.GetAllAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetProductGroupsByProductId_ReturnsGroupsHasProductId_WhenGroupsExists()
    {
        var productId = Guid.NewGuid();
        var productGroup = ProductGroupFactory.Create();
        productGroup.AddProduct(productId);
        _ = await _productGroupRepository.InsertAsync(productGroup);

        var result = await _productGroupRepository.GetProductGroupsByProductIdAsync(productId);

        result.ShouldHaveSingleItem();

    }

    public async Task InitializeAsync() => await _dbReset();
    public Task DisposeAsync() => Task.CompletedTask;
}