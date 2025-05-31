namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class GetAllProductControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task GetAll_ReturnsProducts_WhenProductsExist()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        var createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();

        var products = await _client.GetAsync($"{ProductBaseAddress}");

        products.StatusCode.ShouldBe(HttpStatusCode.OK);
        var productsResponse = await products.Content.ReadFromJsonAsync<IEnumerable<ProductResponse>>();
        productsResponse.ShouldNotBeNull();
        productsResponse.Count().ShouldBe(1);
        productsResponse.First().ShouldBeEquivalentTo(createdProduct);

    }

    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNotProductsExist()
    {
        var products = await _client.GetAsync($"{ProductBaseAddress}");

        products.StatusCode.ShouldBe(HttpStatusCode.OK);
        var productsResponse = await products.Content.ReadFromJsonAsync<IEnumerable<ProductResponse>>();
        productsResponse.ShouldBeEmpty();
    }
}
