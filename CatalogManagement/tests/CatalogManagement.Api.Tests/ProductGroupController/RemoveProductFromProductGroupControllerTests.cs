using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class RemoveProductFromProductGroupControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;
    public RemoveProductFromProductGroupControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        ResetDB();
    }

    [Fact]
    public async Task RemoveProductFromProductGroup_ReturnsProductGroupResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addProductToProductGroupRequest = new AddProductToProductGroupRequest(insertedProduct!.Id);
        await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/add-product", addProductToProductGroupRequest);
        var removeProductFromProductGroupRequest = new RemoveProductFromProductGroupRequest(insertedProduct.Id);

        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/remove-product", removeProductFromProductGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        var data = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
        data.Should().NotBeNull();
        data!.ProductIds.Should().BeEmpty();
    }

    [Fact]
    public async Task ProductHasNoGroupId_WhenRemoveProductFromGroupSuccess()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addProductToProductGroupRequest = new AddProductToProductGroupRequest(insertedProduct!.Id);
        await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/add-product", addProductToProductGroupRequest);
        var removeProductFromProductGroupRequest = new RemoveProductFromProductGroupRequest(insertedProduct!.Id);

        await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/remove-product", removeProductFromProductGroupRequest);

        var productGetResponse = await _client.GetAsync($"http://localhost/api/products/{insertedProduct!.Id}");
        var product = await productGetResponse.Content.ReadFromJsonAsync<ProductResponse>();
        product.Should().NotBeNull();
        product!.GroupIds.Should().BeEmpty();
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task RemoveProductFromProductGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{invalidProductGroupId!}/remove-product", new RemoveProductFromProductGroupRequest(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task RemoveProductFromProductGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var insertedProductGroup = await InsertProductGroup();

        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/remove-product", new RemoveProductFromProductGroupRequest(invalidProductId));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
    }
    [Fact]
    public async Task RemoveProductFromProductGroup_ReturnsNotFound_WhenProductGroupIdNotFound()
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{Guid.NewGuid}/remove-product", new RemoveProductFromProductGroupRequest(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<ProductGroupResponse?> InsertProductGroup()
    {
        var createProductGroupRequest = CreateProductGroupRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createProductGroupRequest);
        return await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
    }

    private async Task<ProductResponse?> InsertProduct()
    {
        var createProductRequest = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", createProductRequest);
        return await response.Content.ReadFromJsonAsync<ProductResponse>();
    }

    private void ResetDB()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
