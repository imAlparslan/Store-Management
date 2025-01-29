using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class DeleteProductGroupControllerTests : ControllerTestBase
{
    public DeleteProductGroupControllerTests(CatalogApiFactory catalogApiFactory):base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenProductGroupExists()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        var createdProductGroup = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var deleteResponse = await _client.DeleteAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

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
        product!.GroupIds.Should().BeEmpty();


    }
    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductGroupNotExists()
    {
        var id = Guid.NewGuid();

        var deleteResponse = await _client.DeleteAsync($"{ProductGroupBaseAddress}/{id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task Delete_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var deleteResponse = await _client.DeleteAsync($"{ProductGroupBaseAddress}/{id}");

        using (AssertionScope scope = new())
        {
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
}