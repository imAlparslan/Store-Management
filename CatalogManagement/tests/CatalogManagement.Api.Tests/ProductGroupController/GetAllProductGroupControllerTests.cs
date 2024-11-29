using CatalogManagement.Api.Tests.RequestFactories;
using CatalogManagement.Contracts.Products;
using CatalogManagement.Infrastructure.Persistence;
using FluentAssertions.Execution;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CatalogManagement.Api.ApiEndpoints;
using CatalogManagement.Contracts.ProductGroups;

namespace CatalogManagement.Api.Tests.ProductGroupController;
public class GetAllProductGroupControllerTests : IClassFixture<CatalogApiFactory>
{
    private readonly HttpClient _client;
    private readonly CatalogApiFactory _catalogApiFactory;

    public GetAllProductGroupControllerTests(CatalogApiFactory catalogApiFactory)
    {
        _client = catalogApiFactory.CreateClient();
        _catalogApiFactory = catalogApiFactory;

        RecreateDb();
    }

    [Fact]
    public async void GetAll_ReturnsProductGroups_WhenProductGroupsExist()
    {
        CreateProductGroupRequest createRequest = CreateProductGroupRequestFactory.CreateValid();
        var createResponse = await _client.PostAsJsonAsync(ProductGroupEndpoints.Create, createRequest);
        var createdProductGroup = await createResponse.Content.ReadFromJsonAsync<ProductGroupResponse>();

        var productGroups = await _client.GetAsync("http://localhost/api/product-groups");

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
    public async void GetAll_ReturnsEmptyResult_WhenNotProductGroupsExist()
    {
        var productGroups = await _client.GetAsync("http://localhost/api/product-groups");

        using (AssertionScope scope = new())
        {
            productGroups.StatusCode.Should().Be(HttpStatusCode.OK);
            var productsResponse = await productGroups.Content.ReadFromJsonAsync<IEnumerable<ProductGroupResponse>>();
            productsResponse.Should().BeEmpty();
        }
    }

    private void RecreateDb()
    {
        var scope = _catalogApiFactory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}
