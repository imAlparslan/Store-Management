using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class AddProductToProductGroupControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;
    public AddProductToProductGroupControllerTests(CatalogApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
        _catalogApiFactory = apiFactory;

        ResetDB();
    }

    [Fact]
    public async Task AddProductToProductGroup_ReturnsProductGroupResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addProductToProductGroupRequest = new AddProductToProductGroupRequest(insertedProduct!.Id);

        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/add-product", addProductToProductGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        var data = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
        data.Should().NotBeNull();
        data!.ProductIds.Should().HaveCount(1);
        data.ProductIds.Should().Contain(insertedProduct.Id);
    }

    [Fact]
    public async Task ProductHasGroupId_WhenAddProductToGroupSuccess()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addProductToProductGroupRequest = new AddProductToProductGroupRequest(insertedProduct!.Id);
        await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/add-product", addProductToProductGroupRequest);
        var productGroupByIdResponse = await _client.GetAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}");
        await productGroupByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var productGetResponse = await _client.GetAsync($"http://localhost/api/products/{insertedProduct!.Id}");
        var product = await productGetResponse.Content.ReadFromJsonAsync<ProductResponse>();

        product.Should().NotBeNull();
        product!.GroupIds.Should().HaveCount(1);
        product!.GroupIds.Should().Contain(insertedProductGroup.Id);
    }
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task AddProductToProductGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{invalidProductGroupId}/add-product", new AddProductToProductGroupRequest(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
    }
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task AddProductToProductGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var insertedProductGroup = await InsertProductGroup();

        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{insertedProductGroup!.Id}/add-product", new AddProductToProductGroupRequest(invalidProductId));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
    }
    [Fact]
    public async Task AddProductToProductGroup_ReturnsNotFound_WhenProductGroupIdNotFound()
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{Guid.NewGuid}/add-product", new AddProductToProductGroupRequest(Guid.NewGuid()));

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
