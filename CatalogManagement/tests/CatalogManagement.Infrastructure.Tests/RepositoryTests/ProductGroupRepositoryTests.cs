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

        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(productGroup);
            result.Should().BeEquivalentTo(inserted);
        }
    }

    [Fact]
    public async Task DeleteById_DeletesAndReturnTrue_WhenIdExists()
    {
        var productGroup = ProductGroupFactory.Create();
        _ = await _productGroupRepository.InsertAsync(productGroup);
        var result = await _productGroupRepository.DeleteByIdAsync(productGroup.Id);
        var isExists = await _productGroupRepository.IsExistsAsync(productGroup.Id);

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            isExists.Should().BeFalse();
        }
    }

    [Fact]
    public async Task InsertProduct_Returns_InsertedProductGroup()
    {
        var productGroup = ProductGroupFactory.Create();

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        var isExists = await _productGroupRepository.IsExistsAsync(productGroup.Id);

        using (new AssertionScope())
        {
            isExists.Should().BeTrue();
            inserted.Id.Should().Be(productGroup.Id);
        }
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

        using (new AssertionScope())
        {
            result.Name.Should().NotBe(oldName);
            result.Name.Should().Be(newName);
            inserted.Name.Should().Be(result.Name);

        }
    }

    [Fact]
    public async Task UpdateProductGroup_ReturnsUpdatedGroup_WhenGroupDefinitionUpdated()
    {
        var productGroup = ProductGroupFactory.Create();
        var oldDescription = productGroup.Description;
        var newDefinition = ProductGroupDescriptionFactory.Create("updated dewfinition");

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        inserted.ChangeDescription(newDefinition);
        ProductGroup result = await _productGroupRepository.UpdateAsync(inserted);

        using (new AssertionScope())
        {
            result.Description.Should().NotBe(oldDescription);
            result.Description.Should().Be(newDefinition);
            inserted.Description.Should().Be(result.Description);

        }
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyCollection_WhenNoProductGroupExists()
    {
        var result = await _productGroupRepository.GetAllAsync();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetProductGroupsByProductId_ReturnsGroupsHasProductId_WhenGroupsExists()
    {
        var productId = Guid.NewGuid();
        var productGroup = ProductGroupFactory.Create();
        productGroup.AddProduct(productId);
        _ = await _productGroupRepository.InsertAsync(productGroup);

        var result = await _productGroupRepository.GetProductGroupsByProductIdAsync(productId);

        result.Should().HaveCount(1);

    }

    public async Task InitializeAsync() => await _dbReset();
    public Task DisposeAsync() => Task.CompletedTask;
}