using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class UpdateProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task Update_UpdatesProductGroup_WhenDataValid()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}", updateRequest);

        var updatedProductResponse = await updatedResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        updatedResponse.ShouldNotBeNull();
        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdNotExists()
    {
        var id = Guid.NewGuid();
        var updateRequest = UpdateProductGroupRequestFactory.CreateValid();

        var response = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress} /{id}", updateRequest);

        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductGroupNameNullOrEmpty(string productGroupName)
    {
        UpdateProductGroupRequest createRequest = UpdateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateWithName(productGroupName);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProduct!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.ShouldHaveSingleItem();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductGroupDescriptionNullOrEmpty(string productGroupDescription)
    {
        UpdateProductGroupRequest createRequest = UpdateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateWithDescription(productGroupDescription);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Update_ReturnsValidationErrors_WhenDataInvalid()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateCustom("", "");

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.Count.ShouldBe(2);
    }

    [Theory]
    [ClassData(typeof(InvalidGuids))]
    public async Task Update_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateValid();
        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{id}", updateRequest);

        updatedResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error.ShouldNotBeNull();
        error.Title.ShouldBe("One or more validation errors occurred.");
        error.Errors.ShouldHaveSingleItem();
    }
}
