using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class AddGroupToProductControllerTests : ControllerTestBase
{

    public AddGroupToProductControllerTests(CatalogApiFactory catalogApiFactory) : base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task AddGroup_ReturnsProductResponse_WhenDataValid()
    {
        var insertedProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct!.Id}/add-group", addGroupRequest);

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
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{insertedProduct!.Id}/add-group", addGroupRequest);

        var productGroupByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{insertedProductGroup.Id}");

        var productGroup = await productGroupByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        productGroup!.ProductIds.Should().HaveCount(1);
        productGroup.ProductIds.Should().Contain(insertedProduct.Id);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task AddGroup_ReturnsBadRequest_WhenProductIdInvalid(Guid invalidProductId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{invalidProductId}/add-group", new AddGroupToProductRequest(Guid.NewGuid()));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        errors.Should().NotBeNull();
        errors!.Errors.Should().HaveCount(1);
        errors.Errors.Should().ContainKey("ProductId");
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task AddGroup_ReturnsBadRequest_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.NewGuid()}/add-group", new AddGroupToProductRequest(invalidProductGroupId));

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
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{invalidProductId}/add-group", new AddGroupToProductRequest(invalidProductGroupId));

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

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}/{Guid.NewGuid()}/add-group", addGroupRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
