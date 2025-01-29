using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class CreateProductControllerTests : ControllerTestBase
{
    public CreateProductControllerTests(CatalogApiFactory catalogApiFactory):base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task Create_ReturnsCreatedProduct_WhenDataValid()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        using (AssertionScope scope = new())
        {
            ProductResponse? productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();
            productResponse.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location!.ToString().Should().Be($"{ProductBaseAddress}/{productResponse!.Id}");
        }
    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductNameNullOrEmpty(string productName)
    {
        var request = CreateProductRequestFactory.CreateWithName(productName);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductCodeNullOrEmpty(string productCode)
    {
        var request = CreateProductRequestFactory.CreateWithCode(productCode);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductDefinitionNullOrEmpty(string productDefinition)
    {
        var request = CreateProductRequestFactory.CreateWithDefinition(productDefinition);

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Fact]
    public async Task Create_ReturnsValidationErrors_WhenDataInvalid()
    {
        var request = CreateProductRequestFactory.CreateCustom("", "", "");

        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(3);
        }
    }
}
