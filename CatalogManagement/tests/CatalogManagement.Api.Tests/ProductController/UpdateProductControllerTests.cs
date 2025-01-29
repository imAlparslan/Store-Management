using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class UpdateProductControllerTests: ControllerTestBase
{
    public UpdateProductControllerTests(CatalogApiFactory catalogApiFactory):base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task Update_UpdatesProduct_WhenDataValid()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            var updatedProductResponse = await updatedResponse.Content.ReadFromJsonAsync<ProductResponse>();
            updatedProductResponse.Should().NotBeNull();
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedProductResponse.Should().BeEquivalentTo(updateRequest);
        }
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdNotExists()
    {
        var id = Guid.NewGuid();
        var updateRequest = UpdateProductRequestFactory.CreateValid();

        var response = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{id}", updateRequest);

        using (AssertionScope scope = new())
        {
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProdoctNameNullOrEmpty(string productName)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithName(productName);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductCodeNullOrEmpty(string productCode)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithCode(productCode);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProdoctDefinitionNullOrEmpty(string productDefinition)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithDefinition(productDefinition);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenDataInvalid()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateCustom("", "", "");

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(3);
        }
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task Update_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductBaseAddress}/{id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
}
