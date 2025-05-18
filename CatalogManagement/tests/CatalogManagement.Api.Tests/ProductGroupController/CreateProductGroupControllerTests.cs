using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class CreateProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task Create_ReturnsCreatedProductGroup_WhenDataValid()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        ProductGroupResponse? productGroupResponse = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
        productGroupResponse.ShouldNotBeNull();
        response.Headers.Location!.ToString().ShouldBe($"{ProductGroupBaseAddress}/{productGroupResponse!.Id}");
        productGroupResponse.ShouldNotBeNull();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductGroupNameNullOrEmpty(string productGroupName)
    {
        var request = CreateProductGroupRequestFactory.CreateWithName(productGroupName);

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductGroupDescriptionNullOrEmpty(string productGroupDescription)
    {
        var request = CreateProductGroupRequestFactory.CreateWithDescription(productGroupDescription);

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(1);
    }

    [Fact]
    public async Task Create_ReturnsValidationErrors_WhenDataInvalid()
    {
        var request = CreateProductGroupRequestFactory.CreateCustom("", "");

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe(400);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(2);
    }
}
