using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class GetByIdProductGroupControllerTests : ControllerTestBase
{
    public GetByIdProductGroupControllerTests(CatalogApiFactory catalogApiFactory):base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task GetById_ReturnsProductGroup_WhenProductGroupExists()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);
        ProductGroupResponse? createdProductGroup = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var getByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}");

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

        var getByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{id}");

        getByIdResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"{ProductGroupBaseAddress}/{id}");
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
