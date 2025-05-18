using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class DeleteProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task Delete_ReturnsOk_WhenProductExists()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var deleteResponse = await _client.DeleteAsync($"{ProductBaseAddress}/{createdProduct!.Id}");

        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

    }

    [Fact]
    public async Task Delete_ProductGroupNotHaveProductId_WhenProductDeleted()
    {
        var createdProduct = await InsertProduct();
        var insertedProductGroup = await InsertProductGroup();
        var addGroupRequest = new AddGroupToProductRequest(insertedProductGroup!.Id);
        await _client.PostAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}/add-group", addGroupRequest);

        await _client.DeleteAsync($"{ProductBaseAddress}/{createdProduct!.Id}");

        var productGroupResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{insertedProductGroup.Id}");
        var productGroup = await productGroupResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        productGroup.ShouldNotBeNull();
        productGroup.ProductIds.ShouldNotContain(createdProduct.Id);

    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductNotExists()
    {
        var id = Guid.NewGuid();

        var deleteResponse = await _client.DeleteAsync($"{ProductBaseAddress}/{id}");

        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);

    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task Delete_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var deleteResponse = await _client.DeleteAsync($"{ProductBaseAddress}/{id}");

        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }
    private async Task<ProductResponse?> InsertProduct()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return createdProduct;
    }

    private async Task<ProductGroupResponse?> InsertProductGroup()
    {
        var createProductGroupRequest = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createProductGroupRequest);
        return await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
    }
}
