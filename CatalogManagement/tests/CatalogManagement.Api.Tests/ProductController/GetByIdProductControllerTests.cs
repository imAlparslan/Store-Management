using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;
public class GetByIdProductControllerTests : IClassFixture<CatalogApiFactory>
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public GetByIdProductControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task GetById_ReturnsProduct_WhenProductExists()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);
        ProductResponse? createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var getByIdResponse = await _client.GetAsync($"http://localhost/api/products/{createdProduct!.Id}");

        using (AssertionScope scope = new())
        {
            ProductResponse? product = await getByIdResponse.Content.ReadFromJsonAsync<ProductResponse>();
            product.Should().NotBeNull();
            getByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Should().BeEquivalentTo(createdProduct);
        }
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenProductNotExists()
    {
        var id = Guid.NewGuid();

        var getByIdResponse = await _client.GetAsync($"http://localhost/api/products/{id}");

        getByIdResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"http://localhost/api/products/{id}");
        using (AssertionScope scope = new())
        {
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
    private void RecreateDb()
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
