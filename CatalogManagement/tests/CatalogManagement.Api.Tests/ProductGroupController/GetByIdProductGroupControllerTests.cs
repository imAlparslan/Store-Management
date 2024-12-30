using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class GetByIdProductGroupControllerTests
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public GetByIdProductGroupControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async Task GetById_ReturnsProductGroup_WhenProductGroupExists()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync("http://localhost/api/product-groups", request);
        ProductGroupResponse? createdProductGroup = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var getByIdResponse = await _client.GetAsync($"http://localhost/api/product-groups/{createdProductGroup!.Id}");

        using (AssertionScope scope = new())
        {
            ProductGroupResponse? productGroup = await getByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
            getByIdResponse.Should().NotBeNull();
            getByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            productGroup.Should().BeEquivalentTo(createdProductGroup);
        }
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenProductGroupNotExists()
    {
        var id = Guid.NewGuid();

        var getByIdResponse = await _client.GetAsync($"http://localhost/api/product-groups/{id}");

        getByIdResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"http://localhost/api/product-groups/{id}");
        using (AssertionScope scope = new())
        {
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
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
