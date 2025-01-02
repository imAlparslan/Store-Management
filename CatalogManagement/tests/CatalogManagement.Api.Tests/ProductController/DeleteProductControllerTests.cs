using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class DeleteProductControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;
    public DeleteProductControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        ResetDB();
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenProductExists()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/products/{createdProduct!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task Delete_ProductGroupNotHaveProductId_WhenProductDeleted()
    {
        var createdProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        await _client.PostAsJsonAsync($"http://localhost/api/products/{createdProduct!.Id}/add-group", addGroupRequest);

        await _client.DeleteAsync($"http://localhost/api/products/{createdProduct!.Id}");

        var productGroupResponse = await _client.GetAsync($"http://localhost/api/product-groups/{insertedProductGroup.Id}");
        var productGroup = await productGroupResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();

        productGroup!.ProductIds.Should().NotContain(createdProduct.Id);

    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductNotExists()
    {
        var id = Guid.NewGuid();

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/products/{id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task Delete_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/products/{id}");
        using (AssertionScope scope = new())
        {
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
    private async Task<ProductResponse?> InsertProduct()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return createdProduct;
    }

    private async Task<ProductGroupResponse?> InsertProductGroup()
    {
        var createProductGroupRequest = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createProductGroupRequest);
        return await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
    }
    private void ResetDB()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
