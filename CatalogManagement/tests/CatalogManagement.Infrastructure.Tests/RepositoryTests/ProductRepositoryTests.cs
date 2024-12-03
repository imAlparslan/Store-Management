using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Infrastructure.Tests.Common.Factories.ProductFactories;
using CatalogManagement.Infrastructure.Tests.Fixtures;

namespace CatalogManagement.Infrastructure.Tests.RepositoryTests;

public class ProductRepositoryTests : IClassFixture<ProductRepositoryFixture>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    public ProductRepositoryTests(ProductRepositoryFixture productRepositoryFixture)
    {
        _productRepository = productRepositoryFixture._productRepository;
        _unitOfWorkManager = productRepositoryFixture._unitOfWorkManager;
    }

    [Fact]
    public async Task Find_By_Id_Should_Return_Correct_Product()
    {
        var product = ProductFactory.CreateRandom();

        var inserted = await _productRepository.InsertAsync(product);
        var result = await _productRepository.GetByIdAsync(product.Id);

        using (new AssertionScope())
        {
            result.Should().BeEquivalentTo(product);
            result.Should().BeEquivalentTo(inserted);
        }
    }

    [Fact]
    public async Task Delete_By_Id_Should_Delete_And_Return_True_When_Id_Correct()
    {
        var product = ProductFactory.CreateRandom();

        var inserted = await _productRepository.InsertAsync(product);
        var result = await _productRepository.DeleteByIdAsync(product.Id);
        var isExists = await _productRepository.IsExistsAsync(product.Id);

        using (new AssertionScope())
        {
            result.Should().BeTrue();
            isExists.Should().BeFalse();
        }
    }

    [Fact]
    public async Task Insert_Product_Should_Return_Inserted_Product()
    {
        var product = ProductFactory.CreateRandom();

        var inserted = await _productRepository.InsertAsync(product);
        var isExists = await _productRepository.IsExistsAsync(product.Id);

        using (new AssertionScope())
        {
            isExists.Should().BeTrue();
            inserted.Id.Should().Be(product.Id);
        }
    }

    [Fact]
    public async Task Update_Product_Name_Should_Return_Product_With_New_Name()
    {
        var product = ProductFactory.CreateRandom();
        var oldName = product.Name;
        var newName = ProductNameFactory.CreateRandom();

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
    public async Task Update_Product_Code_Should_Return_Product_With_New_Code()
    {
        var product = ProductFactory.CreateRandom();
        var oldCode = product.Code;
        var newCode = ProductCodeFactory.CreateRandom();

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
    public async void Update_Product_Definition_Should_Return_Product_With_New_Definition()
    {
        var product = ProductFactory.CreateRandom();
        var oldDefinition = product.Definition;
        var newDefinition = ProductDefinitionFactory.CreateRandom();

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

    public async void Get_All_Should_Return_Collection()
    {
        var result = await _productRepository.GetAllAsync();

        result.Should().BeAssignableTo(typeof(List<Product>));
    }
}