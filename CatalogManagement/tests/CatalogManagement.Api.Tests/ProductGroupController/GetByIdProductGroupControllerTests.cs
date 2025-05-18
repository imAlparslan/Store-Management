using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class GetByIdProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task GetById_ReturnsProductGroup_WhenProductGroupExists()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();
        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);
        ProductGroupResponse? createdProductGroup = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var getByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}");

        ProductGroupResponse? productGroup = await getByIdResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        getByIdResponse.ShouldNotBeNull();
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        productGroup.ShouldBeEquivalentTo(createdProductGroup);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenProductGroupNotExists()
    {
        var id = Guid.NewGuid();

        var getByIdResponse = await _client.GetAsync($"{ProductGroupBaseAddress}/{id}");
        getByIdResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);


    }

    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task GetById_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        var result = await _client.GetAsync($"{ProductGroupBaseAddress}/{id}");

        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error!.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }
}
