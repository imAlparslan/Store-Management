using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class RemoveGroupFromProductControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;
    public RemoveGroupFromProductControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task RemoveGroup_ReturnsProductResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        var addGroupresponse = await _client.PostAsJsonAsync($"http://localhost/api/products/{insertedProduct!.Id}/add-group", addGroupRequest);
        var removeGroupRequest = new RemoveGroupFromProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{insertedProduct.Id}/remove-group", removeGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var data = await response.Content.ReadFromJsonAsync<ProductResponse>();
        data.Should().NotBeNull();
        data!.GroupIds.Should().NotContain(insertedProductGroup!.Id);

    }

    [Fact]
    public async Task RemoveGroup_ProductGroupNotHaveProduct_WhenGroupRemovedFromProduct()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        var addGroupResponse = await _client.PostAsJsonAsync($"http://localhost/api/products/{insertedProduct!.Id}/add-group", addGroupRequest);
        
        var removeGroupRequest = new RemoveGroupFromProductRequest(insertedProductGroup!.Id);
        
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{insertedProduct.Id}/remove-group", removeGroupRequest);
        var productGroupByIdResponse = await _client.GetAsync($"http://localhost/api/product-groups/{insertedProductGroup.Id}");
        var productGroup = await productGroupByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
       
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        productGroup!.ProductIds.Should().NotContain(insertedProduct.Id);
    }

    [Fact]
    public async Task RemoveGroup_ReturnsNotFound_WhenProductIdNotExists()
    {
        var insertedProductGroup = await InsertProductGroup();
        var removeGroupRequest = new RemoveGroupFromProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{Guid.NewGuid}/remove-group", removeGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task RemoveGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{invalidProductId}/remove-group", new AddGroupToProductRequest(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(1);
        errors.Errors.Should().ContainKey("ProductId");
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task RemoveGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{Guid.NewGuid()}/remove-group", new AddGroupToProductRequest(invalidProductGroupId));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(1);
        errors.Errors.Should().ContainKey("GroupId");
    }

    [Fact]
    public async Task RemoveGroup_ReturnsBadRequest_WhenDataInvalid()
    {
        var response = await _client.PostAsJsonAsync($"http://localhost/api/products/{Guid.Empty}/remove-group", new AddGroupToProductRequest(Guid.Empty));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(2);
        errors.Errors.Should().ContainKey("GroupId","ProductId");
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
    private void RecreateDb()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
