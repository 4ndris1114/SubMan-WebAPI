using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Subman.Models;
using Xunit;

namespace Subman.Tests;

public class SubscriptionControllerTests : IClassFixture<CustomWebApplicationFactory<Program>> {
    private readonly HttpClient _client;

    public SubscriptionControllerTests(CustomWebApplicationFactory<Program> factory) {
        _client = factory.CreateClient(); // Creates in-memory test server
    }

    private async Task<string> RegisterAndLoginAsync() {
        // 1. Register a new user
        var registerContent = new UserRegister {
            Username = "testuser",
            Email = "testuser@example.com",
            Password = "Test123!"
        };

        var registerResponse = await _client.PostAsJsonAsync("api/auth/register", registerContent);
        registerResponse.EnsureSuccessStatusCode();

        // 2. Login to get the JWT token
        var loginContent = new UserLogin {
            Email = "testuser@example.com",
            Password = "Test123!"
        };

        var loginResponse = await _client.PostAsJsonAsync("api/auth/login", loginContent);
        loginResponse.EnsureSuccessStatusCode();

        var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();
        
        // Return the token which is the login response
        return loginResponseContent;
    }

    [Fact]
    public async Task CreateSubscription_ReturnsCreatedSubscription() {
        // Arrange
        var token = await RegisterAndLoginAsync();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var newSub = new Subscription {
            UserId = ExtractUserIdFromToken(token),
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

        // Cleanup the user after the test
        await CleanupUserAsync(token);
    }

    private async Task CleanupUserAsync(string token) {
        var userId = ExtractUserIdFromToken(token);
        if (userId == null)
            throw new Exception("User ID could not be extracted from the token.");

        var response = await _client.DeleteAsync($"/api/user/{userId}");

        if (response.IsSuccessStatusCode) {
            Console.WriteLine("User successfully deleted.");
        } else {
            Console.WriteLine($"Failed to delete user: {response.StatusCode}");
        }
    }


    private string ExtractUserIdFromToken(string token) {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        return userId;
    }
}
