using Xunit;
using Moq;
using ORToothFairy.Core.Services;
using ORToothFairy.Core.Repositories;
using ORToothFairy.Core.Entities;

namespace ORToothFairy.Tests.API.Services
{
    public class SearchServiceTests
    {
        // ========== HAPPY PATH TESTS ==========

        [Fact]
        public async Task SearchAsync_WithNoDistanceFilter_ReturnsAllPractitioners()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 45.5,
                    Longitude = -122.6,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 20
                },
                new Practitioner
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Latitude = 45.6,
                    Longitude = -122.7,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 15
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act
            var results = await service.SearchAsync(45.5, -122.6, null);

            // Assert
            Assert.Equal(2, results.Count);
            Assert.Equal("Jane", results[0].FirstName);
        }

        [Fact]
        public async Task SearchAsync_WithDistanceFilter_ReturnsOnlyNearbyPractitioners()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 45.5,
                    Longitude = -122.6,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50  // Within range
                },
                new Practitioner
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Latitude = 46.5,  // ~69 miles away
                    Longitude = -123.0,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 100  // Within range
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act - Search within 10 miles
            var results = await service.SearchAsync(45.5, -122.6, 10);

            // Assert - Only Jane should be returned (John is too far)
            Assert.Single(results);
            Assert.Equal("Jane", results[0].FirstName);
        }

        [Fact]
        public async Task SearchAsync_SortsByDistance_NearestFirst()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Far",
                    LastName = "Away",
                    Latitude = 45.6,  // Farther
                    Longitude = -122.7,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50
                },
                new Practitioner
                {
                    Id = 2,
                    FirstName = "Close",
                    LastName = "By",
                    Latitude = 45.501,  // Very close
                    Longitude = -122.601,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act
            var results = await service.SearchAsync(45.5, -122.6, null);

            // Assert - "Close" should be first
            Assert.Equal(2, results.Count);
            Assert.Equal("Close", results[0].FirstName);
            Assert.Equal("Far", results[1].FirstName);
            Assert.True(results[0].UserPractitionerProximityMiles < results[1].UserPractitionerProximityMiles);
        }

        [Fact]
        public async Task SearchAsync_RespectsMaxTravelMiles_FiltersPractitionersTooFar()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 45.6,  // ~7 miles away
                    Longitude = -122.7,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 5  // Won't travel this far!
                },
                new Practitioner
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Latitude = 45.6,  // ~7 miles away
                    Longitude = -122.7,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 20  // Will travel this far
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act
            var results = await service.SearchAsync(45.5, -122.6, null);

            // Assert - Only John should be returned (Jane's MaxTravelMiles too small)
            Assert.Single(results);
            Assert.Equal("John", results[0].FirstName);
        }

        [Fact]
        public async Task SearchAsync_WithNullMaxTravelMiles_IncludesPractitioner()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 46.0,  // Far away
                    Longitude = -123.0,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = null  // No travel limit
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act
            var results = await service.SearchAsync(45.5, -122.6, null);

            // Assert - Jane should be included (no travel limit)
            Assert.Single(results);
            Assert.Equal("Jane", results[0].FirstName);
        }

        [Fact]
        public async Task SearchAsync_ReturnsEmptyList_WhenNoPractitionersFound()
        {
            // Arrange - No practitioners in Oregon
            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(new List<Practitioner>());

            var service = new SearchService(mockRepository.Object);

            // Act
            var results = await service.SearchAsync(45.5, -122.6, null);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public async Task SearchAsync_CalculatesDistanceCorrectly()
        {
            // Arrange - Known distance calculation
            // Portland (45.5152, -122.6784) to Beaverton (45.4875, -122.8037)
            // Expected: ~10 miles
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 45.4875,
                    Longitude = -122.8037,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act
            var results = await service.SearchAsync(45.5152, -122.6784, null);

            // Assert - Distance should be approximately 10 miles
            Assert.Single(results);
            Assert.InRange(results[0].UserPractitionerProximityMiles, 6.0, 7.0);  // Actual distance is ~6.4 miles
        }

        // ========== EDGE CASE TESTS ==========

        [Fact]
        public async Task SearchAsync_HandlesIdenticalLocations_ReturnsZeroDistance()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 45.5,
                    Longitude = -122.6,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act - Search at exact same location
            var results = await service.SearchAsync(45.5, -122.6, null);

            // Assert
            Assert.Single(results);
            Assert.Equal(0.0, results[0].UserPractitionerProximityMiles, precision: 1);
        }

        [Fact]
        public async Task SearchAsync_WithZeroDistanceFilter_ReturnsOnlyExactMatches()
        {
            // Arrange
            var mockPractitioners = new List<Practitioner>
            {
                new Practitioner
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Latitude = 45.5,
                    Longitude = -122.6,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50
                },
                new Practitioner
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Latitude = 45.501,  // Slightly different
                    Longitude = -122.601,
                    IsActive = true,
                    State = "OR",
                    MaxTravelMiles = 50
                }
            };

            var mockRepository = new Mock<IPractitionerRepository>();
            mockRepository.Setup(r => r.GetActivePractitionersAsync("OR"))
                .ReturnsAsync(mockPractitioners);

            var service = new SearchService(mockRepository.Object);

            // Act - Search with 0 mile filter (edge case, but valid)
            var results = await service.SearchAsync(45.5, -122.6, 0);

            // Assert - Only exact match should be returned
            Assert.Single(results);
            Assert.Equal("Jane", results[0].FirstName);
        }
    }
}