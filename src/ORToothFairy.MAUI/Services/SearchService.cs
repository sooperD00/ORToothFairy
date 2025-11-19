using System.Text.Json;
using ORToothFairy.Core.Models;

namespace ORToothFairy.MAUI.Services;

public class SearchService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public SearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        // Configure JSON deserialization
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<PractitionerSearchResult>> SearchByCoordinatesAsync(
    double latitude,
    double longitude,
    int userSearchRadiusMiles = 25)
    {
        var url = $"api/search?latitude={latitude}&longitude={longitude}&userSearchRadiusMiles={userSearchRadiusMiles}";
        return await GetPractitionersAsync(url);
    }

    public async Task<List<PractitionerSearchResult>> SearchByZipCodeAsync(
        string zipCode,
        int userSearchRadiusMiles = 25)
    {
        var url = $"api/search?zipCode={zipCode}&userSearchRadiusMiles={userSearchRadiusMiles}";
        return await GetPractitionersAsync(url);
    }

    public async Task<List<PractitionerSearchResult>> SearchByAddressAsync(
        string address,
        int userSearchRadiusMiles = 25)
    {
        var encodedAddress = Uri.EscapeDataString(address);
        var url = $"api/search?address={encodedAddress}&userSearchRadiusMiles={userSearchRadiusMiles}";
        return await GetPractitionersAsync(url);
    }

    private async Task<List<PractitionerSearchResult>> GetPractitionersAsync(string url)
    {
        try
        {
            var fullUrl = $"{_httpClient.BaseAddress}{url}";
            Console.WriteLine($"Calling API: {fullUrl}");

            var response = await _httpClient.GetAsync(url);

            Console.WriteLine($"Response status: {response.StatusCode}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response JSON: {json}");

            // ADD THIS: Deserialize the wrapper object first
            var wrapper = JsonSerializer.Deserialize<SearchResultWrapper>(json, _jsonOptions);

            return wrapper?.Results ?? new List<PractitionerSearchResult>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Error: {ex.Message}");
            throw;
        }
    }
}
internal class SearchResultWrapper
{
    public List<PractitionerSearchResult> Results { get; set; } = new();
    public int Count { get; set; }
    public string? SearchLocation { get; set; }
    public int? DistanceFilter { get; set; }
}