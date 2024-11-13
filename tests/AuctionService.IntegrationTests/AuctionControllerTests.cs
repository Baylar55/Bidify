using System.Net;
using System.Net.Http.Json;
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.IntegrationTests.Fixtures;
using AuctionService.IntegrationTests.Util;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionService.IntegrationTests;

[Collection("SharedFixtures")]
public class AuctionControllerTests : IAsyncLifetime
{
    private readonly CustomWebAppFactory _factory;
    private readonly HttpClient _client;
    private const string FORDGT_ID = "afbee524-5972-4075-8800-7d1f9d7b0a0c";

    public AuctionControllerTests(CustomWebAppFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAuctions_ShouldReturn3Auctions()
    {
        // Arrange?

        // Act
        var response = await _client.GetFromJsonAsync<List<AuctionDto>>("api/auctions");

        // Assert
        Assert.Equal(10, response.Count);
    }

    [Fact]
    public async Task GetAuctionById_WithValidId_ShouldReturnAuction()
    {
        // Arrange?

        // Act
        var response = await _client.GetFromJsonAsync<AuctionDto>($"api/auctions/{FORDGT_ID}");

        // Assert
        Assert.Equal(FORDGT_ID, response.Model);
    }

    [Fact]
    public async Task GetAuctionById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange?

        // Act
        var response = await _client.GetAsync($"api/auctions/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAuctionById_WithInvalidGuid_ShouldReturnBadRequest()
    {
        // Arrange?

        // Act
        var response = await _client.GetAsync("api/auctions/invalid-guid");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateAuction_WithNoAuth_ShouldReturnUnauthorized()
    {
        // Arrange
        var newAuction = new CreateAuctionDto { Make = "Test" };

        // Act
        var response = await _client.PostAsJsonAsync("api/auctions", newAuction);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateAuction_WithAuth_ShouldReturnCreated()
    {
        // Arrange
        var newAuction = GetAuctionForCreate();
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("test-user"));

        // Act
        var response = await _client.PostAsJsonAsync("api/auctions", newAuction);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdAuction = await response.Content.ReadFromJsonAsync<AuctionDto>();
        Assert.Equal("test-user", createdAuction.Seller);    
    }

        [Fact]
    public async Task CreateAuction_WithInvalidCreateAuctionDto_ShouldReturnBadRequest()
    {
        // Arrange
        var newAuction = GetAuctionForCreate();
        newAuction.Make = null;
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("test-user"));

        // Act
        var response = await _client.PostAsJsonAsync("api/auctions", newAuction);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAuction_WithValidUpdateDtoAndUser_ShouldReturnOk()
    {
        // Arrange
        var updateAuction = new UpdateAuctionDto { Make = "Updated" };
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("test-user"));

        // Act
        var response = await _client.PutAsJsonAsync($"api/auctions/{FORDGT_ID}", updateAuction);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAuction_WithValidUpdateDtoAndInvalidUser_ShouldReturnForbidden()
    {
        // Arrange 
        var updateAuction = new UpdateAuctionDto { Make = "Updated" };
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("invalid-user"));

        // Act
        var response = await _client.PutAsJsonAsync($"api/auctions/{FORDGT_ID}", updateAuction);

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    private static CreateAuctionDto GetAuctionForCreate(){
        return new CreateAuctionDto{
            Make = "Test",
            Model = "Test",
            Year = 2022,
            ImageUrl = "https://www.google.com",
            Color = "Test",
            Mileage = 1000,
            ReservePrice = 10000,
        };
    }

    public Task DisposeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
        DbHelper.ReinitDbForTests(dbContext);
        return Task.CompletedTask;
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
