using Moq;
using Subman.Controllers;
using Subman.Models;
using Subman.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Subman.Database; // Assuming your MongoDbContext is here

public class SubscriptionControllerTests
{
    private readonly SubscriptionController _controller;
    private readonly Mock<SubscriptionRepository> _mockRepo;
    private readonly Mock<ILogger<SubscriptionController>> _mockLogger;

    public SubscriptionControllerTests()
    {
        string mockConnectionString = "mongodb://localhost:27017/testdb"; // This is a mock string, no need for a real DB

        // Mock MongoDbContext with the mock connection string
        var mockDbContext = new MongoDbContext(mockConnectionString);

        // Mock the SubscriptionRepository with the mocked DbContext
        _mockRepo = new Mock<SubscriptionRepository>(mockDbContext);
        
        // Mock logger
        _mockLogger = new Mock<ILogger<SubscriptionController>>();

        // Instantiate the controller with mocked dependencies
        _controller = new SubscriptionController(_mockRepo.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithSubscriptions()
    {
        // Arrange
        var subscriptions = new List<Subscription>
        {
            new Subscription { Id = "1", Name = "Subscription 1" },
            new Subscription { Id = "2", Name = "Subscription 2" }
        };

        // Mock the GetAllAsync method of the SubscriptionRepository to return the test subscriptions
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(subscriptions);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Subscription>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedSubscriptions = Assert.IsType<List<Subscription>>(okResult.Value);
        Assert.Equal(2, returnedSubscriptions.Count); // Ensure 2 subscriptions are returned
    }
}
