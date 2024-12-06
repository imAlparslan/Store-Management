using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;
public class GetAllProductControllerTests : IClassFixture<CatalogApiFactory>
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public GetAllProductControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task GetAll_ReturnsProducts_WhenProductsExist()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        var createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();

        var products = await _client.GetAsync("http://localhost/api/products");

        using (AssertionScope scope = new())
        {
            products.StatusCode.Should().Be(HttpStatusCode.OK);
            var productsResponse = await products.Content.ReadFromJsonAsync<IEnumerable<ProductResponse>>();
            productsResponse.Should().NotBeNullOrEmpty();
            productsResponse!.Count().Should().Be(1);
            productsResponse!.First().Should().BeEquivalentTo(createdProduct);

        }
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNotProductsExist()
    {
        var products = await _client.GetAsync("http://localhost/api/products");

        using (AssertionScope scope = new())
        {
            products.StatusCode.Should().Be(HttpStatusCode.OK);
            var productsResponse = await products.Content.ReadFromJsonAsync<IEnumerable<ProductResponse>>();
            productsResponse.Should().BeEmpty();
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
