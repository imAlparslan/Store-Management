﻿using FluentAssertions.Execution;
using StoreDefinition.Api.Tests.Common;
using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.ShopsController;

[Collection(nameof(ShopsControllerCollectionFixture))]
public class CreateShopControllerTests(StoreDefinitionApiFactory apiFactory)
    : ControllerTestBase(apiFactory)
{
    [Fact]
    public async Task Create_ReturnsCreatedProduct_WhenRequestValid()
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest();

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var shopResponse = await response.Content.ReadFromJsonAsync<ShopResponse>();
            shopResponse.Should().NotBeNull();
            response.Headers.Location!.Should().Be($"{ShopsBaseAddress}/{shopResponse!.Id}");
        }
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Create_ValidationError_WhenShopDescriptionInvalid(string invalid)
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest(shopDescription: invalid);

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error!.Errors.Should().HaveCount(1);
        }
    }


    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Create_ValidationError_WhenShopCityInvalid(string invalid)
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest(city: invalid);

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error!.Errors.Should().HaveCount(1);
        }
    }


    [Theory]
    [MemberData(nameof(invalidStrings))]
    public async Task Create_ValidationError_WhenShopStreetInvalid(string invalid)
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest(street: invalid);

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error!.Errors.Should().HaveCount(1);
        }
    }


    [Fact]
    public async Task Create_ValidationError_WhenShopRequestInvalid()
    {
        CreateShopRequest request = ShopsRequestFactory.CreateShopCreateRequest("", "", "");

        var response = await _client.PostAsJsonAsync(ShopsBaseAddress, request);

        using (AssertionScope scope = new())
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error.Should().NotBeNull();
            error!.Status.Should().Be(400);
            error!.Errors.Should().HaveCount(3);
        }
    }
}
