using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class UpdateProductGroupControllerTests : ControllerTestBase
{
    public UpdateProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : base(catalogApiFactory)
    {
    }


    [Fact]
    public async Task Update_UpdatesProductGroup_WhenDataValid()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateValid();

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}", updateRequest);

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

        var response = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress} /{id}", updateRequest);

        using (AssertionScope scope = new())
        {
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductGroupNameNullOrEmpty(string productGroupName)
    {
        UpdateProductGroupRequest createRequest = UpdateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProduct = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateWithName(productGroupName);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProduct!.Id}", updateRequest);

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
    [MemberData(nameof(InvalidStrings))]
    public async Task Update_ReturnsValidationError_WhenProductGroupDescriptionNullOrEmpty(string productGroupDescription)
    {
        UpdateProductGroupRequest createRequest = UpdateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateWithDescription(productGroupDescription);

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}", updateRequest);

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
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        ProductGroupResponse? createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateCustom("", "");

        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{createdProductGroup!.Id}", updateRequest);

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
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task Update_ReturnsValidationError_WhenIdInvalid(Guid id)
    {
        UpdateProductGroupRequest updateRequest = UpdateProductGroupRequestFactory.CreateValid();
        var updatedResponse = await _client.PutAsJsonAsync($"{ProductGroupBaseAddress}/{id}", updateRequest);
        using (AssertionScope scope = new())
        {
            updatedResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await updatedResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Title.Should().Be("One or more validation errors occurred.");
            error.Errors.Count.Should().Be(1);
        }
    }
}
