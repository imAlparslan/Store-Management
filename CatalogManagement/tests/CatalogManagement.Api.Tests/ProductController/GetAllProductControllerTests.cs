using CatalogManagement.Api.Tests.RequestFactories;
using CatalogManagement.Contracts.Products;
using CatalogManagement.Infrastructure.Persistence;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using static CatalogManagement.Api.ApiEndpoints;

namespace CatalogManagement.Api.Tests.ProductController;
public class GetAllProductControllerTests : IClassFixture<CatalogApiFactory>
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _productApiFactory;

    public GetAllProductControllerTests(CatalogApiFactory productApiFactory)
    {
        _client = productApiFactory.CreateClient();
        _productApiFactory = productApiFactory;

        RecreateDb();
    }

    [Fact]
    public async void GetAll_ReturnsProducts_WhenProductsExist()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync(ProductEndpoints.Create, createRequest);
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
    public async void GetAll_ReturnsEmptyResult_WhenNotProductsExist()
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
        var scope = _productApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
