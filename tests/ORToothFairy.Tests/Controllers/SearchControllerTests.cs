using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ORToothFairy.API.Controllers;
using ORToothFairy.Core.Services;
using ORToothFairy.Core.Models;

namespace ORToothFairy.Tests.Controllers
{
    public class SearchControllerTests
    {
        private readonly Mock<ISearchService> _mockSearchService;
        private readonly Mock<IGeocodingService> _mockGeocodingService;
        private readonly SearchController _controller;

        public SearchControllerTests()
        {
            _mockSearchService = new Mock<ISearchService>();
            _mockGeocodingService = new Mock<IGeocodingService>();
            _controller = new SearchController(_mockSearchService.Object, _mockGeocodingService.Object);
        }

        // ========== HAPPY PATH TESTS ==========

        [Fact]
        public async Task Search_WithValidLatLon_ReturnsResults()
        {
            // Arrange
            var mockResults = new List<PractitionerSearchResult>
            {
                new PractitionerSearchResult
                {
                    PractitionerId = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Phone = "503-555-1234",
                    Email = "jane@example.com",
                    AcceptsCalls = true,
                    AcceptsTexts = false,
                    Services = new List<string> { "Cleanings" },
                    UserPractitionerProximityMiles = 2.3
                }
            };

            _mockSearchService
                .Setup(s => s.SearchAsync(45.5, -122.6, null))
                .ReturnsAsync(mockResults);

            // Act
            var result = await _controller.Search(
                latitude: 45.5,
                longitude: -122.6,
                zipCode: null,
                address: null,
                distanceMiles: null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchResponse>(okResult.Value);
            Assert.Single(response.Results);
            Assert.Equal("Jane", response.Results[0].FirstName);
            Assert.Equal(2.3, response.Results[0].UserPractitionerProximityMiles);
        }

        [Fact]
        public async Task Search_WithValidZipCode_GeocodesAndReturnsResults()
        {
            // Arrange
            var geocodingResult = new GeocodingResult
            {
                Latitude = 45.4875,
                Longitude = -122.8037,
                DisplayName = "Beaverton, OR 97006"
            };

            _mockGeocodingService
                .Setup(g => g.GeocodeZipCodeAsync("97006"))
                .ReturnsAsync(geocodingResult);

            var mockResults = new List<PractitionerSearchResult>
            {
                new PractitionerSearchResult
                {
                    PractitionerId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "503-555-5678",
                    Email = "john@example.com",
                    AcceptsCalls = false,
                    AcceptsTexts = true,
                    Services = new List<string> { "Sealants" },
                    UserPractitionerProximityMiles = 1.5
                }
            };

            _mockSearchService
                .Setup(s => s.SearchAsync(45.4875, -122.8037, null))
                .ReturnsAsync(mockResults);

            // Act
            var result = await _controller.Search(
                latitude: null,
                longitude: null,
                zipCode: "97006",
                address: null,
                distanceMiles: null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchResponse>(okResult.Value);
            Assert.Single(response.Results);
            Assert.Equal("John", response.Results[0].FirstName);
            Assert.Equal("Beaverton, OR 97006", response.SearchLocation);
        }

        [Fact]
        public async Task Search_WithValidAddress_GeocodesAndReturnsResults()
        {
            // Arrange
            var geocodingResult = new GeocodingResult
            {
                Latitude = 45.5152,
                Longitude = -122.6784,
                DisplayName = "Portland, OR"
            };

            _mockGeocodingService
                .Setup(g => g.GeocodeAddressAsync("123 Main St, Portland OR"))
                .ReturnsAsync(geocodingResult);

            var mockResults = new List<PractitionerSearchResult>
            {
                new PractitionerSearchResult
                {
                    PractitionerId = 2,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Phone = "503-555-9999",
                    Email = "alice@example.com",
                    AcceptsCalls = true,
                    AcceptsTexts = true,
                    Services = new List<string> { "Cleanings", "Exams" },
                    UserPractitionerProximityMiles = 0.8
                }
            };

            _mockSearchService
                .Setup(s => s.SearchAsync(45.5152, -122.6784, null))
                .ReturnsAsync(mockResults);

            // Act
            var result = await _controller.Search(
                latitude: null,
                longitude: null,
                zipCode: null,
                address: "123 Main St, Portland OR",
                distanceMiles: null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchResponse>(okResult.Value);
            Assert.Single(response.Results);
            Assert.Equal("Alice", response.Results[0].FirstName);
            Assert.Equal("Portland, OR", response.SearchLocation);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public async Task Search_WithDistanceFilter_PassesFilterToService(int distanceMiles)
        {
            // Arrange
            _mockSearchService
                .Setup(s => s.SearchAsync(45.5, -122.6, distanceMiles))
                .ReturnsAsync(new List<PractitionerSearchResult>());

            // Act
            var result = await _controller.Search(
                latitude: 45.5,
                longitude: -122.6,
                zipCode: null,
                address: null,
                distanceMiles: distanceMiles);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchResponse>(okResult.Value);
            Assert.Equal(distanceMiles, response.DistanceFilter);

            // Verify the service was called with the correct distance filter
            _mockSearchService.Verify(
                s => s.SearchAsync(45.5, -122.6, distanceMiles),
                Times.Once);
        }

        [Fact]
        public async Task Search_WithoutDistanceFilter_ReturnsAllResults()
        {
            // Arrange
            var mockResults = new List<PractitionerSearchResult>
            {
                new PractitionerSearchResult { PractitionerId = 1, FirstName = "Jane", LastName = "Smith", UserPractitionerProximityMiles = 2.3 },
                new PractitionerSearchResult { PractitionerId = 2, FirstName = "John", LastName = "Doe", UserPractitionerProximityMiles = 15.7 },
                new PractitionerSearchResult { PractitionerId = 3, FirstName = "Alice", LastName = "Johnson", UserPractitionerProximityMiles = 25.1 }
            };

            _mockSearchService
                .Setup(s => s.SearchAsync(45.5, -122.6, null))
                .ReturnsAsync(mockResults);

            // Act
            var result = await _controller.Search(
                latitude: 45.5,
                longitude: -122.6,
                zipCode: null,
                address: null,
                distanceMiles: null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SearchResponse>(okResult.Value);
            Assert.Equal(3, response.Count);
            Assert.Null(response.DistanceFilter); // No filter applied
        }

        // ========== ERROR CASE TESTS ==========

        [Fact]
        public async Task Search_WithNoLocationProvided_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Search(
                latitude: null,
                longitude: null,
                zipCode: null,
                address: null,
                distanceMiles: null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = badRequestResult.Value;
            Assert.NotNull(errorResponse);

            // Check error message contains expected text
            var errorMessage = errorResponse.GetType().GetProperty("error")?.GetValue(errorResponse)?.ToString();
            Assert.Contains("Must provide either", errorMessage);
        }

        [Theory]
        [InlineData(-91, -122.6)] // Latitude too low
        [InlineData(91, -122.6)]  // Latitude too high
        [InlineData(45.5, -181)]  // Longitude too low
        [InlineData(45.5, 181)]   // Longitude too high
        public async Task Search_WithInvalidLatLon_ReturnsBadRequest(double lat, double lon)
        {
            // Act
            var result = await _controller.Search(
                latitude: lat,
                longitude: lon,
                zipCode: null,
                address: null,
                distanceMiles: null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task Search_WithInvalidZipCode_ReturnsBadRequest()
        {
            // Arrange
            _mockGeocodingService
                .Setup(g => g.GeocodeZipCodeAsync("00000"))
                .ReturnsAsync((GeocodingResult?)null); // Geocoding failed

            // Act
            var result = await _controller.Search(
                latitude: null,
                longitude: null,
                zipCode: "00000",
                address: null,
                distanceMiles: null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = badRequestResult.Value;
            Assert.NotNull(errorResponse);

            var errorMessage = errorResponse.GetType().GetProperty("error")?.GetValue(errorResponse)?.ToString();
            Assert.Contains("Could not find location", errorMessage);
        }

        [Fact]
        public async Task Search_WithInvalidAddress_ReturnsBadRequest()
        {
            // Arrange
            _mockGeocodingService
                .Setup(g => g.GeocodeAddressAsync("Fake Address That Does Not Exist"))
                .ReturnsAsync((GeocodingResult?)null); // Geocoding failed

            // Act
            var result = await _controller.Search(
                latitude: null,
                longitude: null,
                zipCode: null,
                address: "Fake Address That Does Not Exist",
                distanceMiles: null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = badRequestResult.Value;
            Assert.NotNull(errorResponse);

            var errorMessage = errorResponse.GetType().GetProperty("error")?.GetValue(errorResponse)?.ToString();
            Assert.Contains("Could not find location", errorMessage);
        }

        [Theory]
        [InlineData(3)]   // Not a valid option
        [InlineData(15)]  // Not a valid option
        [InlineData(100)] // Not a valid option
        public async Task Search_WithInvalidDistanceFilter_ReturnsBadRequest(int invalidDistance)
        {
            // Act
            var result = await _controller.Search(
                latitude: 45.5,
                longitude: -122.6,
                zipCode: null,
                address: null,
                distanceMiles: invalidDistance);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = badRequestResult.Value;
            Assert.NotNull(errorResponse);

            var errorMessage = errorResponse.GetType().GetProperty("error")?.GetValue(errorResponse)?.ToString();
            Assert.Contains("Distance must be 5, 10, or 20", errorMessage);
        }

        // ========== EDGE CASE TESTS ==========

        [Fact]
        public async Task Search_WithBothZipCodeAndAddress_PrioritizesZipCode()
        {
            // Arrange - Only zip code geocoding should be called
            var geocodingResult = new GeocodingResult
            {
                Latitude = 45.4875,
                Longitude = -122.8037,
                DisplayName = "Beaverton, OR 97006"
            };

            _mockGeocodingService
                .Setup(g => g.GeocodeZipCodeAsync("97006"))
                .ReturnsAsync(geocodingResult);

            _mockSearchService
                .Setup(s => s.SearchAsync(45.4875, -122.8037, null))
                .ReturnsAsync(new List<PractitionerSearchResult>());

            // Act
            var result = await _controller.Search(
                latitude: null,
                longitude: null,
                zipCode: "97006",
                address: "123 Main St, Portland OR", // This should be ignored
                distanceMiles: null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Verify zip code was used, address was NOT called
            _mockGeocodingService.Verify(g => g.GeocodeZipCodeAsync("97006"), Times.Once);
            _mockGeocodingService.Verify(g => g.GeocodeAddressAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Search_WithLatLonAndZipCode_PrioritizesLatLon()
        {
            // Arrange - Only direct lat/lon should be used (no geocoding)
            _mockSearchService
                .Setup(s => s.SearchAsync(45.5, -122.6, null))
                .ReturnsAsync(new List<PractitionerSearchResult>());

            // Act
            var result = await _controller.Search(
                latitude: 45.5,
                longitude: -122.6,
                zipCode: "97006", // This should be ignored
                address: null,
                distanceMiles: null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Verify no geocoding was called
            _mockGeocodingService.Verify(g => g.GeocodeZipCodeAsync(It.IsAny<string>()), Times.Never);
            _mockGeocodingService.Verify(g => g.GeocodeAddressAsync(It.IsAny<string>()), Times.Never);
        }
    }
}