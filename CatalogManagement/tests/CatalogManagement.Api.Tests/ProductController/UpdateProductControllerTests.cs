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
public class UpdateProductControllerTests : IClassFixture<ProductApiFactory>
{
    private readonly HttpClient _client;
    private readonly ProductApiFactory _productApiFactory;

    public UpdateProductControllerTests(ProductApiFactory productApiFactory)
    {
        _client = productApiFactory.CreateClient();
        _productApiFactory = productApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task Update_UpdatesProduct_WhenDataValid()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/products/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            var updatedProductResponse = await updatedResponse.Content.ReadFromJsonAsync<ProductResponse>();
            updatedResponse.Should().NotBeNull();
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedProductResponse.Should().BeEquivalentTo(updateRequest);
        }
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdNotExists()
    {
        var id = Guid.NewGuid();
        var updateRequest = UpdateProductRequestFactory.CreateValid();

        var response = await _client.PutAsJsonAsync($"http://localhost/api/products/{id}", updateRequest);

        using (AssertionScope scope = new())
        {
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Update_ReturnsValidationError_WhenProdoctNameNullOrEmpty(string productName)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithName(productName);

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/products/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Update_ReturnsValidationError_WhenProdoctCodeNullOrEmpty(string productCode)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithCode(productCode);

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/products/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Update_ReturnsValidationError_WhenProdoctDefinitionNullOrEmpty(string productDefinition)
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateWithDefinition(productDefinition);

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/products/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenDataInvalid()
    {
        CreateProductRequest createRequest = CreateProductRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/products", createRequest);
        ProductResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateCustom("", "", "");

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/products/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(3);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async void Update_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        UpdateProductRequest updateRequest = UpdateProductRequestFactory.CreateValid();
        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/products/{id}", updateRequest);
        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
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
