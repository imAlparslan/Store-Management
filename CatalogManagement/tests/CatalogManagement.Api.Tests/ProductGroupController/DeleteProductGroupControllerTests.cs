using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class DeleteProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task Delete_ReturnsOk_WhenProductGroupExists()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        var createdProductGroup = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var deleteResponse = await _client.DeleteAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}");

        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

    }

    [Fact]
    public async Task ProductHasNoGroupId_WhenProductGroupDeleted()
    {
        var createProductRequest = CreateProductRequestFactory.CreateValid();
        var postProductResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createProductRequest);
        var createdProduct = await postProductResponse.Content.ReadFromJsonAsync<ProductResponse>();
        var createProductGroupRequest = CreateProductGroupRequestFactory.CreateValid();
        var postProductGroupResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createProductGroupRequest);
        var createdProductGroup = await postProductGroupResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        var addProductToProductGroupRequest = new AddProductToProductGroupRequest(createdProduct!.Id);
        await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}/add-product", addProductToProductGroupRequest);

        await _client.DeleteAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}");

        var productResponse = await _client.GetAsync($"{ProductBaseAddress}/{createdProduct.Id}");
        var product = await productResponse.Content.ReadFromJsonAsync<ProductResponse>();
        product.ShouldNotBeNull();
        product.GroupIds.ShouldBeEmpty();

    }
    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductGroupNotExists()
    {
        var id = Guid.NewGuid();

        var deleteResponse = await _client.DeleteAsync($"{ProductGroupBaseAddress}/{id}");

        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);

    }
    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task Delete_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var deleteResponse = await _client.DeleteAsync($"{ProductGroupBaseAddress}/{id}");

        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }
}