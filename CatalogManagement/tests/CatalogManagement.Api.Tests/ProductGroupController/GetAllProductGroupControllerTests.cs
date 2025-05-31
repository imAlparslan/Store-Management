using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;

[Collection(nameof(ProductGroupControllerCollectionFixture))]
public class GetAllProductGroupControllerTests(CatalogApiFactory catalogApiFactory) : ControllerTestBase(catalogApiFactory)
{
    [Fact]
    public async Task GetAll_ReturnsProductGroups_WhenProductGroupsExist()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync($"{ProductGroupBaseAddress}", createRequest);
        var createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var productGroups = await _client.GetAsync($"{ProductGroupBaseAddress}");

        productGroups.StatusCode.ShouldBe(HttpStatusCode.OK);
        var productsResponse = await productGroups.Content.ReadFromJsonAsync<IEnumerable<ProductGroupResponse>>();
        productsResponse.ShouldNotBeNull();
        productsResponse.ShouldHaveSingleItem();
        productsResponse!.First().ShouldBeEquivalentTo(createdProductGroup);

    }

    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNotProductGroupsExist()
    {
        var productGroups = await _client.GetAsync($"{ProductGroupBaseAddress}");

        productGroups.StatusCode.ShouldBe(HttpStatusCode.OK);
        var productsResponse = await productGroups.Content.ReadFromJsonAsync<IEnumerable<ProductGroupResponse>>();
        productsResponse.ShouldNotBeNull();
        productsResponse.ShouldBeEmpty();
    }
}
