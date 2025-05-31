namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class GetByIdProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task GetById_ReturnsProduct_WhenProductExists()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);
        ProductResponse? createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var getByIdResponse = await _client.GetAsync($"{ProductBaseAddress}/{createdProduct!.Id}");

        ProductResponse? product = await getByIdResponse.Content.ReadFromJsonAsync<ProductResponse>();
        product.ShouldNotBeNull();
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        product.ShouldBeEquivalentTo(createdProduct);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenProductNotExists()
    {
        var id = Guid.NewGuid();

        var getByIdResponse = await _client.GetAsync($"{ProductBaseAddress}/{id}");

        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);

    }

    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"{ProductBaseAddress}/{id}");

        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }
}
