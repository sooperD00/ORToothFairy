using System.Net.Http.Json;
using ORToothFairy.Core.Models;

namespace ORToothFairy.Web.Services;

public class SearchApiClient
{
    private readonly HttpClient _http;

    public SearchApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PractitionerSearchResult>> SearchByCoordinatesAsync(
        double latitude, double longitude, int? maxDistanceMiles = null)
    {
        var url = $"api/search?latitude={latitude}&longitude={longitude}";
        if (maxDistanceMiles.HasValue)
            url += $"&userSearchRadiusMiles={maxDistanceMiles}";

        var response = await _http.GetFromJsonAsync<SearchResponse>(url);
        return response?.Results ?? new List<PractitionerSearchResult>();
    }

    public async Task<List<PractitionerSearchResult>> SearchByZipCodeAsync(
        string zipCode, int? maxDistanceMiles = null)
    {
        var url = $"api/search?zipCode={zipCode}";
        if (maxDistanceMiles.HasValue)
            url += $"&userSearchRadiusMiles={maxDistanceMiles}";

        var response = await _http.GetFromJsonAsync<SearchResponse>(url);
        return response?.Results ?? new List<PractitionerSearchResult>();
    }

    public async Task<List<PractitionerSearchResult>> SearchByAddressAsync(
        string address, int? maxDistanceMiles = null)
    {
        var url = $"api/search?address={Uri.EscapeDataString(address)}";
        if (maxDistanceMiles.HasValue)
            url += $"&userSearchRadiusMiles={maxDistanceMiles}";

        var response = await _http.GetFromJsonAsync<SearchResponse>(url);
        return response?.Results ?? new List<PractitionerSearchResult>();
    }

    private class SearchResponse
    {
        public List<PractitionerSearchResult> Results { get; set; } = new();
        public int Count { get; set; }
        public string? SearchLocation { get; set; }
        public int? DistanceFilter { get; set; }
    }
}
