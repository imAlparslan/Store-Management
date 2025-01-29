using CatalogManagement.Api.Tests.Common;
using CatalogManagement.Api.Tests.Fixtures;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class GetAllProductGroupControllerTests: ControllerTestBase
{
    public GetAllProductGroupControllerTests(CatalogApiFactory catalogApiFactory):base(catalogApiFactory)
    {
    }

    [Fact]
    public async Task GetAll_ReturnsProductGroups_WhenProductGroupsExist()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        var createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var productGroups = await _client.GetAsync($"{ProductGroupBaseAddress}");

        using (AssertionScope scope = new())
        {
            productGroups.StatusCode.Should().Be(HttpStatusCode.OK);
            var productsResponse = await productGroups.Content.ReadFromJsonAsync<IEnumerable<ProductGroupResponse>>();
            productsResponse.Should().NotBeNullOrEmpty();
            productsResponse!.Count().Should().Be(1);
            productsResponse!.First().Should().BeEquivalentTo(createdProductGroup);

        }
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNotProductGroupsExist()
    {
        var productGroups = await _client.GetAsync($"{ProductGroupBaseAddress}");

        using (AssertionScope scope = new())
        {
            productGroups.StatusCode.Should().Be(HttpStatusCode.OK);
            var productsResponse = await productGroups.Content.ReadFromJsonAsync<IEnumerable<ProductGroupResponse>>();
            productsResponse.Should().BeEmpty();
        }
    }
}
