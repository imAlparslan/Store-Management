using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class RemoveGroupFromProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task RemoveGroup_ReturnsProductResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct!.Id}/add-group", addGroupRequest);
        var removeGroupRequest = new RemoveGroupFromProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct.Id}/remove-group", removeGroupRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var data = await response.Content.ReadFromJsonAsync<ProductResponse>();
        data.ShouldNotBeNull();
        data.GroupIds.ShouldNotContain(insertedProductGroup!.Id);

    }

    [Fact]
    public async Task RemoveGroup_ProductGroupNotHaveProduct_WhenGroupRemovedFromProduct()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct!.Id}/add-group", addGroupRequest);
        var removeGroupRequest = new RemoveGroupFromProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct.Id}/remove-group", removeGroupRequest);

        var productGroupByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{insertedProductGroup.Id}");
        var productGroup = await productGroupByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        productGroup.ShouldNotBeNull();
        productGroup.ProductIds.ShouldNotContain(insertedProduct.Id);
    }

    [Fact]
    public async Task RemoveGroup_ReturnsNotFound_WhenProductIdNotExists()
    {
        var insertedProductGroup = await InsertProductGroup();
        var removeGroupRequest = new RemoveGroupFromProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.NewGuid}/remove-group", removeGroupRequest);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

    }
    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task RemoveGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{invalidProductId}/remove-group", new AddGroupToProductRequest(Guid.NewGuid()));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.ShouldNotBeNull();
        errors.Errors.Count.ShouldBe(1);
        errors.Errors.ShouldContainKey("ProductId");
    }

    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task RemoveGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.NewGuid()}/remove-group", new AddGroupToProductRequest(invalidProductGroupId));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.ShouldNotBeNull();
        errors.Errors.Count.ShouldBe(1);
        errors.Errors.ShouldContainKey("GroupId");
    }

    [Fact]
    public async Task RemoveGroup_ReturnsBadRequest_WhenDataInvalid()
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.Empty}/remove-group", new AddGroupToProductRequest(Guid.Empty));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.ShouldNotBeNull();
        errors.Errors.Count.ShouldBe(2);
        errors.Errors.ShouldContainKey("GroupId", "ProductId");
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
