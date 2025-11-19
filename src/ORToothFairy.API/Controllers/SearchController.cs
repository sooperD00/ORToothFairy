using Microsoft.AspNetCore.Mvc;
using ORToothFairy.Core.Services;
using ORToothFairy.Core.Models;

namespace ORToothFairy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly IGeocodingService _geocodingService;

    public SearchController(ISearchService searchService, IGeocodingService geocodingService)
    {
        _searchService = searchService;
        _geocodingService = geocodingService;
    }

    /// <summary>
    /// Search for EDHS practitioners by location
    /// Accepts 3 input methods: direct lat/lon, zip code, or full address
    /// </summary>
    /// <param name="latitude">Direct latitude (use with longitude)</param>
    /// <param name="longitude">Direct longitude (use with latitude)</param>
    /// <param name="zipCode">US zip code (5 digits)</param>
    /// <param name="address">Full address (street, city, state)</param>
    /// <param name="userSearchRadiusMiles">Filter by distance (5, 10, 20, or null for all)</param>
    /// <returns>List of practitioners sorted by distance</returns>
    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] double? latitude,
        [FromQuery] double? longitude,
        [FromQuery] string? zipCode,
        [FromQuery] string? address,
        [FromQuery] int? userSearchRadiusMiles)
    {
        double lat, lon;
        string? locationUsed = null;

        // STEP 1: Determine which input method the user provided
        // Priority: lat/lon > zipCode > address (only one should be provided)

        if (latitude.HasValue && longitude.HasValue)
        {
            // Input Method 1: Direct lat/lon from device geolocation
            lat = latitude.Value;
            lon = longitude.Value;
            locationUsed = $"({lat}, {lon})";

            // Validate lat/lon are in valid ranges
            if (lat < -90 || lat > 90)
            {
                return BadRequest(new { error = "Latitude must be between -90 and 90" });
            }
            if (lon < -180 || lon > 180)
            {
                return BadRequest(new { error = "Longitude must be between -180 and 180" });
            }
        }
        else if (!string.IsNullOrWhiteSpace(zipCode))
        {
            // Input Method 2: Zip code -> geocode to lat/lon
            var result = await _geocodingService.GeocodeZipCodeAsync(zipCode);

            if (result == null)
            {
                return BadRequest(new { error = $"Could not find location for zip code: {zipCode}" });
            }

            lat = result.Latitude;
            lon = result.Longitude;
            locationUsed = result.DisplayName;
        }
        else if (!string.IsNullOrWhiteSpace(address))
        {
            // Input Method 3: Full address -> geocode to lat/lon
            var result = await _geocodingService.GeocodeAddressAsync(address);

            if (result == null)
            {
                return BadRequest(new { error = $"Could not find location for address: {address}" });
            }

            lat = result.Latitude;
            lon = result.Longitude;
            locationUsed = result.DisplayName;
        }
        else
        {
            // No location provided at all
            return BadRequest(new
            {
                error = "Must provide either latitude+longitude, zipCode, or address"
            });
        }

        // STEP 2: Validate distance filter (if provided)
        if (userSearchRadiusMiles.HasValue && userSearchRadiusMiles.Value <= 0)
        {
            return BadRequest(new { error = "Distance must be greater than 0" });
        }

        // STEP 3: Call YOUR existing SearchService
        Console.WriteLine($"[Controller] Calling SearchService with userSearchRadiusMiles={userSearchRadiusMiles}");
        var results = await _searchService.SearchAsync(lat, lon, userSearchRadiusMiles);

        // STEP 4: Build response with metadata
        var response = new SearchResponse
        {
            Results = results,
            Count = results.Count,
            SearchLocation = locationUsed,
            DistanceFilter = userSearchRadiusMiles
        };

        return Ok(response);
    }
}

// Response wrapper (adds metadata like search location and count)
public class SearchResponse
{
    public List<PractitionerSearchResult> Results { get; set; } = new();
    public int Count { get; set; }
    public string? SearchLocation { get; set; } // e.g. "Beaverton, OR 97006"
    public int? DistanceFilter { get; set; } // e.g. 10 (miles)
}