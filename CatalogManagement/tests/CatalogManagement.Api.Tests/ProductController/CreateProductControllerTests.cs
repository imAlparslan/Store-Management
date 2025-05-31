namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class CreateProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task Create_ReturnsCreatedProduct_WhenDataValid()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location!.ToString().ShouldStartWith(ProductBaseAddress);
        var productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();
        productResponse.ShouldNotBeNull();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductNameNullOrEmpty(string productName)
    {
        var request = CreateProductRequestFactory.CreateWithName(productName);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldNotBeNull();
        error.ShouldSatisfyAllConditions(
            () => error.Status.Value.ShouldBe((int)HttpStatusCode.BadRequest),
            () => error.Title.ShouldBe("One or more validation errors occurred."),
            () => error.Errors.Count.ShouldBe(1)
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductCodeNullOrEmpty(string productCode)
    {
        var request = CreateProductRequestFactory.CreateWithCode(productCode);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldNotBeNull();
        error.ShouldSatisfyAllConditions(
            () => error.Status.Value.ShouldBe((int)HttpStatusCode.BadRequest),
            () => error.Title.ShouldBe("One or more validation errors occurred."),
            () => error.Errors.Count.ShouldBe(1)
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductDefinitionNullOrEmpty(string productDefinition)
    {
        var request = CreateProductRequestFactory.CreateWithDefinition(productDefinition);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldNotBeNull();
        error.ShouldSatisfyAllConditions(
            () => error.Status.Value.ShouldBe((int)HttpStatusCode.BadRequest),
            () => error.Title.ShouldBe("One or more validation errors occurred."),
            () => error.Errors.Count.ShouldBe(1)
        );
    }

    [Fact]
    public async Task Create_ReturnsValidationErrors_WhenDataInvalid()
    {
        var request = CreateProductRequestFactory.CreateCustom("", "", "");

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldNotBeNull();

        error.ShouldSatisfyAllConditions(
            () => error.Status.Value.ShouldBe((int)HttpStatusCode.BadRequest),
            () => error.Title.ShouldBe("One or more validation errors occurred."),
            () => error.Errors.Count.ShouldBe(3)
        );
    }
}
