using CatalogManagement.Contracts.ProductGroups;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductGroupController;
public class DeleteProductGroupControllerTests : IClassFixture<CatalogApiFactory>
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;
    public DeleteProductGroupControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        ResetDB();
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenProductGroupExists()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createRequest);
        var createdProductGroup = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task ProductHasNoGroupId_WhenProductGroupDeleted()
    {
        var createProductRequest = CreateProductRequestFactory.CreateValid();
        var postProductResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createProductRequest);
        var createdProduct = await postProductResponse.Content.ReadFromJsonAsync<ProductResponse>();
        var createProductGroupRequest = CreateProductGroupRequestFactory.CreateValid();
        var postProductGroupResponse = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createProductGroupRequest);
        var createdProductGroup = await postProductGroupResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        var addProductToProductGroupRequest = new AddProductToProductGroupRequest(createdProduct.Id);
        await _client.PostAsJsonAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}/add-product", addProductToProductGroupRequest);

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}");

        var productResponse = await _client.GetAsync($"http://localhost/api/products/{createdProduct.Id}");
        var product = await productResponse.Content.ReadFromJsonAsync<ProductResponse>();
        product!.GroupIds.Should().BeEmpty();


    }
    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductGroupNotExists()
    {
        var id = Guid.NewGuid();

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/product-groups/{id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async Task Delete_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/product-groups/{id}");
        using (AssertionScope scope = new())
        {
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await deleteResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
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