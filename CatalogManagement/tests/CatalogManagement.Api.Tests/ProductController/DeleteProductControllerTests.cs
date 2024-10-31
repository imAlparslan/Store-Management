using CatalogManagement.Api.Tests.RequestFactories;
using CatalogManagement.Contracts.Products;
using CatalogManagement.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace CatalogManagement.Api.Tests.ProductController;
public class DeleteProductControllerTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _client;
    private readonly ProductApiFactory _productApiFactory;
    public DeleteProductControllerTests(ProductApiFactory productApiFactory)
    {
        _client = productApiFactory.CreateClient();
        _productApiFactory = productApiFactory;

        RecreateDb();
    }

    [Fact]
    public async void Delete_ReturnsOk_WhenProductExists()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/products/{createdProduct!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    }
    [Fact]
    public async void Delete_ReturnsNotFound_WhenProductNotExists()
    {
        var id = Guid.NewGuid();

        var deleteResponse = await _client.DeleteAsync($"http://localhost/api/products/{id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    private void RecreateDb()
    {
        var scope = _productApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
