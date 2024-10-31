using CatalogManagement.Api.Tests.RequestFactories;
using CatalogManagement.Contracts.Products;
using CatalogManagement.Infrastructure.Persistence;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace CatalogManagement.Api.Tests.ProductController;
public class CreateProductControllerTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _client;
    private readonly ProductApiFactory _productApiFactory;

    public CreateProductControllerTests(ProductApiFactory productApiFactory)
    {
        _client = productApiFactory.CreateClient();
        _productApiFactory = productApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task Create_CreatesProduct_WhenDataValid()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

        using (AssertionScope scope = new())
        {
            ProductResponse? productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location!.ToString().Should().Be($"http://localhost/api/products/{productResponse!.Id}");
            productResponse.Should().NotBeNull();
            productResponse.Should().BeEquivalentTo(request);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsError_WhenProdoctNameNullOrEmpty(string productName)
    {
        var request = CreateProductRequestFactory.CreateWithName(productName);

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsError_WhenProdoctCodeNullOrEmpty(string productCode)
    {
        var request = CreateProductRequestFactory.CreateWithCode(productCode);

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsError_WhenProdoctDefinitionNullOrEmpty(string productDefinition)
    {
        var request = CreateProductRequestFactory.CreateWithDefinition(productDefinition);

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Fact]
    public async Task Create_ReturnsError_WhenDataInValid()
    {
        var request = CreateProductRequestFactory.CreateCustom("", "", "");

        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(3);
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
