using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
using CatalogManagement.Infrastructure.Tests.Common.Factories.ProductGroupFactories;
using CatalogManagement.Infrastructure.Tests.Fixtures;

namespace CatalogManagement.Infrastructure.Tests.RepositoryTests;
public class ProductGroupRepositoryTests : IClassFixture<ProductGroupRepositoryFixture>
{
    private readonly IProductGroupRepository _productGroupRepository;
    public ProductGroupRepositoryTests(ProductGroupRepositoryFixture productGroupRepositoryFixture)
    {
        _productGroupRepository = productGroupRepositoryFixture._productGroupRepository;

        productGroupRepositoryFixture.RecreateDb();
    }

    [Fact]
    public async Task Find_By_Id_Should_Return_Correct_ProductGroup()
    {
        var productGroup = ProductGroupFactory.CreateRandom();

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        var result = await _productGroupRepository.GetByIdAsync(productGroup.Id);

        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(productGroup);
            result.Should().BeEquivalentTo(inserted);
        }
    }

    [Fact]
    public async Task Delete_By_Id_Should_Delete_And_Return_True_When_Id_Correct()
    {
        var productGroup = ProductGroupFactory.CreateRandom();
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
    public async Task Insert_Product_Should_Return_Inserted_ProductGroup()
    {
        var productGroup = ProductGroupFactory.CreateRandom();

        var inserted = await _productGroupRepository.InsertAsync(productGroup);
        var isExists = await _productGroupRepository.IsExistsAsync(productGroup.Id);

        using (new AssertionScope())
        {
            isExists.Should().BeTrue();
            inserted.Id.Should().Be(productGroup.Id);
        }
    }

    [Fact]
    public async Task Update_ProductGroup_Name_Should_Return_ProductGroup_With_New_Name()
    {
        var productGroup = ProductGroupFactory.CreateRandom();
        var oldName = productGroup.Name;
        var newName = ProductGroupNameFactory.CreateRandom();

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
    public async Task Update_ProductGroup_Definition_Should_Return_ProductGroup_With_New_Definition()
    {
        var productGroup = ProductGroupFactory.CreateRandom();
        var oldDescription = productGroup.Description;
        var newDefinition = ProductGroupDescriptionFactory.CreateRandom();

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
    public async Task Get_All_Should_Return_Collection()
    {
        var result = await _productGroupRepository.GetAllAsync();

        result.Should().BeAssignableTo(typeof(IEnumerable<ProductGroup>));
    }

    [Fact]
    public async Task Get_ProductGroups_By_Containing_Product_Should_Return_Collection()
    {
        var productId = Guid.NewGuid();
        var productGroup = ProductGroupFactory.CreateRandom();
        productGroup.AddProduct(productId);
        _ = await _productGroupRepository.InsertAsync(productGroup);

        var result = await _productGroupRepository.GetProductGroupsByContainigProductAsync(productId);

        result.Should().HaveCount(1);

    }
}