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
public class GetByIdProductControllerTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _client;
    private readonly ProductApiFactory _productApiFactory;

    public GetByIdProductControllerTests(ProductApiFactory productApiFactory)
    {
        _client = productApiFactory.CreateClient();
        _productApiFactory = productApiFactory;

        RecreateDb();
    }

    [Fact]
    public async void GetById_ReturnsProduct_WhenProductExists()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/products", request);
        ProductResponse? createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var getByIdResponse = await _client.GetAsync($"http://localhost/api/products/{createdProduct!.Id}");

        using (AssertionScope scope = new())
        {
            ProductResponse? product = await getByIdResponse.Content.ReadFromJsonAsync<ProductResponse>();
            getByIdResponse.Should().NotBeNull();
            getByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Should().BeEquivalentTo(createdProduct);
        }
    }

    [Fact]
    public async void GetById_ReturnsNotFound_WhenProductNotExists()
    {
        var id = Guid.NewGuid();

        var getByIdResponse = await _client.GetAsync($"http://localhost/api/products/{id}");

        getByIdResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async void GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"http://localhost/api/products/{id}");
        using (AssertionScope scope = new())
        {
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
    private void RecreateDb()
    {
        var scope = _productApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
