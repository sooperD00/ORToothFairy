using Microsoft.EntityFrameworkCore;
using Moq;
using ORToothFairy.API.Data;
using ORToothFairy.API.Services;
using ORToothFairy.Core.Entities;
using ORToothFairy.Tests.Helpers;
using Xunit;

namespace ORToothFairy.Tests.API.Services;

public class SearchServiceTests
{
    [Fact]
    public async Task SearchAsync_Should_Return_Practitioners_Within_Distance()
    {
        // Arrange - Create fake practitioners
        var practitioners = new List<Practitioner>
        {
            new Practitioner
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Close",
                Email = "jane@example.com",
                Phone = "503-555-0001",
                Latitude = 45.5231,  // ~1 mile from user
                Longitude = -122.6765,
                MaxTravelMiles = 10,
                AcceptsTexts = true,
                AcceptsCalls = true,
                IsActive = true
            },
            new Practitioner
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Far",
                Email = "bob@example.com",
                Phone = "503-555-0002",
                Latitude = 45.6,  // ~5 miles from user
                Longitude = -122.8,
                MaxTravelMiles = 30,
                AcceptsTexts = false,
                AcceptsCalls = true,
                IsActive = true
            }
        };

        // Create a mock DbSet that supports async operations
        var mockDbSet = CreateMockDbSet(practitioners.AsQueryable());

        // Mock the DbContext
        var mockContext = new Mock<ApplicationDbContext>(
            new DbContextOptionsBuilder<ApplicationDbContext>().Options);

        mockContext.Setup(c => c.Practitioners).Returns(mockDbSet.Object);

        // Create SearchService with mocked context
        var searchService = new SearchService(mockContext.Object);

        // Act - Search from downtown Portland
        var results = await searchService.SearchAsync(45.5231, -122.6765, userSearchRadiusMiles: 15);

        // Assert - Should return both practitioners (both within 15 miles)
        Assert.Equal(2, results.Count);
        Assert.Equal("Jane", results[0].FirstName); // Closest first
        Assert.True(results[0].UserPractitionerProximityMiles < results[1].UserPractitionerProximityMiles);
    }

    // Helper method to create a mockable DbSet with async support
    private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<T>(data.Provider));
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

        return mockSet;
    }
}