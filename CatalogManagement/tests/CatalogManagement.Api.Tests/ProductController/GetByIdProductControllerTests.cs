using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.Products;

namespace CatalogManagement.Api.Tests.ProductController;

[Collection(nameof(ProductControllerCollectionFixture))]
public class GetByIdProductControllerTests : ControllerTestBase
{
    public GetByIdProductControllerTests(CatalogApiFactory catalogApiFactory) : base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task GetById_ReturnsProduct_WhenProductExists()
    {
        CreateProductRequest request = CreateProductRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductBaseAddress}", request);
        ProductResponse? createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();

        var getByIdResponse = await _client.GetAsync($"{ProductBaseAddress}/{createdProduct!.Id}");

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

        var getByIdResponse = await _client.GetAsync($"{ProductBaseAddress}/{id}");

        getByIdResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"{ProductBaseAddress}/{id}");
        using (AssertionScope scope = new())
        {
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
}
