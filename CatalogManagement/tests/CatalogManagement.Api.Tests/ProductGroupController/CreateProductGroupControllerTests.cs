using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;
public class CreateProductGroupControllerTests : IClassFixture<CatalogApiFactory>
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;
    public CreateProductGroupControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        ReCreateDb();
    }

    [Fact]
    public async Task Create_ReturnsCreatedProductGroup_WhenDataValid()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            ProductGroupResponse? productGroupResponse = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
            productGroupResponse.Should().NotBeNull();
            response.Headers.Location!.ToString().Should().Be($"http://localhost/api/product-groups/{productGroupResponse!.Id}");
            productGroupResponse.Should().NotBeNull();
            productGroupResponse.Should().BeEquivalentTo(request);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsValidationError_WhenProdoctGroupNameNullOrEmpty(string productGroupName)
    {
        var request = CreateProductGroupRequestFactory.CreateWithName(productGroupName);

        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Create_ReturnsValidationError_WhenProductGroupDescriptionNullOrEmpty(string productGroupDescription)
    {
        var request = CreateProductGroupRequestFactory.CreateWithDescription(productGroupDescription);

        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Fact]
    public async Task Create_ReturnsValidationErrors_WhenDataInvalid()
    {
        var request = CreateProductGroupRequestFactory.CreateCustom("", "");

        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(2);
        }
    }
    private void ReCreateDb()
    {
        var scope = _catalogApiFactory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}
