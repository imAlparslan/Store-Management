using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class GetAllProductControllerTests : ControllerTestBase
{
    public GetAllProductControllerTests(CatalogApiFactory catalogApiFactory) : base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task GetAll_ReturnsProducts_WhenProductsExist()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductBaseAddress}", createRequest);
        var createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();

        var products = await _client.GetAsync($"{ProductBaseAddress}");

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
        var products = await _client.GetAsync($"{ProductBaseAddress}");

        using (AssertionScope scope = new())
        {
            products.StatusCode.Should().Be(HttpStatusCode.OK);
            var productsResponse = await products.Content.ReadFromJsonAsync<IEnumerable<ProductResponse>>();
            productsResponse.Should().BeEmpty();
        }
    }
}
