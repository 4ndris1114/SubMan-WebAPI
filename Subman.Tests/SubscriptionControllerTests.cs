using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Subman.Models;
using Xunit;

namespace Subman.Tests;

public class SubscriptionControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SubscriptionControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(); // Creates in-memory test server
    }

    [Fact]
    public async Task CreateSubscription_ReturnsCreatedSubscription()
    {
        // Arrange
        var newSub = new Subscription
        {
            UserId = "67ee48ee6e4ea3dcd7a71fd7", //this is a dummy Id
            Name = "Test Netflix",
            Description = "Monthly Test Netflix subscription",
            Price = 12.99d,
            Currency = "USD",
            StartDate = DateTime.UtcNow.AddMonths(1),
            Interval = 31,
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/subscription", newSub);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        var returnedSub = await response.Content.ReadFromJsonAsync<Subscription>();
        returnedSub.Should().NotBeNull();
        returnedSub!.Name.Should().Be(newSub.Name);
        returnedSub.UserId.Should().Be(newSub.UserId);
    }
}
