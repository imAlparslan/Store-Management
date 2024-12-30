using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class UpdateProductGroupControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public UpdateProductGroupControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task Update_UpdatesProductGroup_WhenDataValid()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            var updatedProductResponse = await updatedResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
            updatedResponse.Should().NotBeNull();
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedProductResponse.Should().BeEquivalentTo(updateRequest);
        }
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdNotExists()
    {
        var id = Guid.NewGuid();
        var updateRequest = UpdateProductGroupRequestFactory.CreateValid();

        var response = await _client.PutAsJsonAsync($"http://localhost/api/product-groups/{id}", updateRequest);

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
    public async Task Update_ReturnsValidationError_WhenProdoctGroupNameNullOrEmpty(string productGroupName)
    {
        UpdateProductGroupRequest createRequest = UpdateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createRequest);
        ProductGroupResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateWithName(productGroupName);

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/product-groups/{createdProduct!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
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
    public async Task Update_ReturnsValidationError_WhenProdoctGroupDescriptionNullOrEmpty(string productGroupDescriotion)
    {
        UpdateProductGroupRequest createRequest = UpdateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateWithDescription(productGroupDescriotion);

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenDataInvalid()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync("http://localhost/api/product-groups", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateCustom("", "");

        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}", updateRequest);

        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(2);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async void Update_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateValid();
        var updatedResponse = await _client.PutAsJsonAsync($"http://localhost/api/product-groups/{id}", updateRequest);
        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }

    private void RecreateDb()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null! },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
