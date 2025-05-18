namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class UpdateProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task Update_UpdatesProduct_WhenDataValid()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        var updatedProductResponse = await updatedResponse.Content.ReadFromJsonAsync<ProductResponse>();
        updatedProductResponse.ShouldNotBeNull();
        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdNotExists()
    {
        var id = Guid.NewGuid();
        var updateRequest = UpdateProductRequestFactory.CreateValid();

        var response = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{id}", updateRequest);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductNameNullOrEmpty(string productName)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithName(productName);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductCodeNullOrEmpty(string productCode)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithCode(productCode);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProdUctDefinitionNullOrEmpty(string productDefinition)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithDefinition(productDefinition);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe(400);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenDataInvalid()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateCustom("", "", "");

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(3);
    }

    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task Update_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }
}
