using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class CreateProductGroupControllerTests : ControllerTestBase
{
    public CreateProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task Create_ReturnsCreatedProductGroup_WhenDataValid()
    {
        CreateProductGroupRequest request = CreateProductGroupRequestFactory.CreateValid();

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            ProductGroupResponse? productGroupResponse = await response.Content.ReadFromJsonAsync<ProductGroupResponse>();
            productGroupResponse.Should().NotBeNull();
            response.Headers.Location!.ToString().Should().Be($"{ProductGroupBaseAddress}/{productGroupResponse!.Id}");
            productGroupResponse.Should().NotBeNull();
            productGroupResponse.Should().BeEquivalentTo(request);
        }
    }

    [Theory]
    [MemberData(nameof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProdoctGroupNameNullOrEmpty(string productGroupName)
    {
        var request = CreateProductGroupRequestFactory.CreateWithName(productGroupName);

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

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
    [MemberData(nameof(InvalidStrings))]
    public async Task Create_ReturnsValidationError_WhenProductGroupDescriptionNullOrEmpty(string productGroupDescription)
    {
        var request = CreateProductGroupRequestFactory.CreateWithDescription(productGroupDescription);

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

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

        var response = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", request);

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
}
