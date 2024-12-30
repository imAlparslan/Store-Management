using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class CreateProductControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public CreateProductControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task Create_ReturnsCreatedProduct_WhenDataValid()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

        using (AssertionScope scope = new())
        {
            ProductResponse? productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();
            productResponse.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location!.ToString().Should().Be($"http://localhost/api/products/{productResponse!.Id}");
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsValidationError_WhenProductNameNullOrEmpty(string productName)
    {
        var request = CreateProductRequestFactory.CreateWithName(productName);

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsValidationError_WhenProdoctCodeNullOrEmpty(string productCode)
    {
        var request = CreateProductRequestFactory.CreateWithCode(productCode);

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

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
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsValidationError_WhenProdoctDefinitionNullOrEmpty(string productDefinition)
    {
        var request = CreateProductRequestFactory.CreateWithDefinition(productDefinition);

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

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

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

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

    private void RecreateDb()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
