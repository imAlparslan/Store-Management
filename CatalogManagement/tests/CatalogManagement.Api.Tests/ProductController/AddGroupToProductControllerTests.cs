using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class AddGroupToProductControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public AddGroupToProductControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        ResetDB();

    }

    [Fact]
    public async Task AddGroup_ReturnsProductResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{insertedProduct!.Id}/add-group", addGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        var data = await response.Content.ReadFromJsonAsync<ProductResponse>();
        data.Should().NotBeNull();
        data!.GroupIds.Should().HaveCount(1);
        data.GroupIds.Should().Contain(insertedProductGroup.Id);
    }

    [Fact]
    public async Task AddGroup_ProductGroupHasProduct_WhenNewGroupAddedToProduct()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{insertedProduct!.Id}/add-group", addGroupRequest);

        var productGroupByIdResponse = await _client.GetAsync($"http://localhost/api/product-groups/{insertedProductGroup.Id}");

        var productGroup = await productGroupByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        productGroup!.ProductIds.Should().HaveCount(1);
        productGroup.ProductIds.Should().Contain(insertedProduct.Id);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task AddGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{invalidProductId}/add-group", new AddGroupToProductRequest(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(1);
        errors.Errors.Should().ContainKey("ProductId");
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async Task AddGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{Guid.NewGuid()}/add-group", new AddGroupToProductRequest(invalidProductGroupId));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(1);
        errors.Errors.Should().ContainKey("GroupId");
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000")]
    public async Task AddGroup_ReturnsBadRequest_WhenDataInvalid(Guid invalidProductId, Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{invalidProductId}/add-group", new AddGroupToProductRequest(invalidProductGroupId));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(2);
        errors.Errors.Should().ContainKey("ProductId");
        errors.Errors.Should().ContainKey("GroupId");
    }

    [Fact]
    public async Task AddGroup_ReturnsNotFound_WhenProductIdNotExists()
    {
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{Guid.NewGuid()}/add-group", addGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<ProductResponse?> InsertProduct()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);
        return await response.Content.ReadFromJsonAsync<ProductResponse>();

    }
    private async Task<ProductGroupResponse?> InsertProductGroup()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();

        HttpResponseMessage response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", request);
        return await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

    }
    private void ResetDB()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null! },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
