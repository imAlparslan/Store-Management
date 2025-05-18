using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class AddGroupToProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{

    [Fact]
    public async Task AddGroup_ReturnsProductResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct!.Id}/add-group", addGroupRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.ShouldNotBeNull();
        var data = await response.Content.ReadFromJsonAsync<ProductResponse>();
        data.ShouldNotBeNull();
        data.GroupIds.ShouldNotBeEmpty();
        data.GroupIds.ShouldContain(insertedProductGroup.Id);
    }

    [Fact]
    public async Task AddGroup_ProductGroupHasProduct_WhenNewGroupAddedToProduct()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct!.Id}/add-group", addGroupRequest);

        var productGroupByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{insertedProductGroup.Id}");

        var productGroup = await productGroupByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        productGroup.ShouldNotBeNull();
        productGroup.ProductIds.ShouldNotBeEmpty();
        productGroup.ProductIds.ShouldContain(insertedProduct.Id);
    }

    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task AddGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{invalidProductId}/add-group", new AddGroupToProductRequest(Guid.NewGuid()));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.ShouldNotBeNull();
        errors.Errors.ShouldNotBeEmpty();
        errors.Errors.ShouldContainKey("ProductId");
    }

    [Theory]
    [ClassData(typeof(InvalidData.InvalidGuids))]
    public async Task AddGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.NewGuid()}/add-group", new AddGroupToProductRequest(invalidProductGroupId));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.ShouldNotBeNull();
        errors.Errors.ShouldHaveSingleItem();
        errors.Errors.ShouldContainKey("GroupId");
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000")]
    public async Task AddGroup_ReturnsBadRequest_WhenDataInvalid(Guid invalidProductId, Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{invalidProductId}/add-group", new AddGroupToProductRequest(invalidProductGroupId));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.ShouldNotBeNull();
        errors.Errors.Count.ShouldBe(2);
        errors.Errors.ShouldContainKey("ProductId");
        errors.Errors.ShouldContainKey("GroupId");
    }

    [Fact]
    public async Task AddGroup_ReturnsNotFound_WhenProductIdNotExists()
    {
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.NewGuid()}/add-group", addGroupRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    private async Task<ProductResponse?> InsertProduct()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        return await response.Content.ReadFromJsonAsync<ProductResponse>();

    }
    private async Task<ProductGroupResponse?> InsertProductGroup()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();

        HttpResponseMessage response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);
        return await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
    }
}
