using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ORToothFairy.Core.Services
{
    /// <summary>
    /// Converts zip codes and addresses to latitude/longitude coordinates
    /// Uses Nominatim (OpenStreetMap) - free, no API key required
    /// </summary>
    public interface IGeocodingService
    {
        Task<GeocodingResult?> GeocodeZipCodeAsync(string zipCode);
        Task<GeocodingResult?> GeocodeAddressAsync(string address);
    }

    public class GeocodingService : IGeocodingService
    {
        private readonly HttpClient _httpClient;
        private const string NominatimBaseUrl = "https://nominatim.openstreetmap.org";

        public GeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Nominatim requires a User-Agent header (be a good API citizen)
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ORToothFairy/1.0 (contact@ortoothfairy.com)");
        }

        /// <summary>
        /// Converts a US zip code to lat/lon
        /// Example: "97006" -> (45.4875, -122.8037)
        /// </summary>
        public async Task<GeocodingResult?> GeocodeZipCodeAsync(string zipCode)
        {
            // Clean up zip code (remove spaces, dashes)
            zipCode = zipCode.Trim().Replace("-", "").Replace(" ", "");

            // Validate it's 5 digits
            if (zipCode.Length != 5 || !zipCode.All(char.IsDigit))
            {
                return null; // Invalid zip format
            }

            // Nominatim search URL (restrict to US postal codes)
            var url = $"{NominatimBaseUrl}/search?postalcode={zipCode}&country=US&format=json&limit=1";

            try
            {
                var results = await _httpClient.GetFromJsonAsync<List<NominatimResponse>>(url);

                if (results == null || results.Count == 0)
                {
                    return null; // Zip code not found
                }

                var first = results[0];
                return new GeocodingResult
                {
                    Latitude = double.Parse(first.Lat),
                    Longitude = double.Parse(first.Lon),
                    DisplayName = first.DisplayName
                };
            }
            catch (HttpRequestException)
            {
                return null; // Network error
            }
        }

        /// <summary>
        /// Converts a full address to lat/lon
        /// Example: "123 Main St, Portland OR" -> (45.5152, -122.6784)
        /// </summary>
        public async Task<GeocodingResult?> GeocodeAddressAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            // URL encode the address
            var encodedAddress = Uri.EscapeDataString(address);

            // Nominatim search URL (restrict to US)
            var url = $"{NominatimBaseUrl}/search?q={encodedAddress}&countrycodes=us&format=json&limit=1";

            try
            {
                var results = await _httpClient.GetFromJsonAsync<List<NominatimResponse>>(url);

                if (results == null || results.Count == 0)
                {
                    return null; // Address not found
                }

                var first = results[0];
                return new GeocodingResult
                {
                    Latitude = double.Parse(first.Lat),
                    Longitude = double.Parse(first.Lon),
                    DisplayName = first.DisplayName
                };
            }
            catch (HttpRequestException)
            {
                return null; // Network error
            }
        }

        // Nominatim JSON response structure
        private class NominatimResponse
        {
            [JsonPropertyName("lat")]
            public string Lat { get; set; } = string.Empty;

            [JsonPropertyName("lon")]
            public string Lon { get; set; } = string.Empty;

            [JsonPropertyName("display_name")]
            public string DisplayName { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// Result of geocoding operation
    /// </summary>
    public class GeocodingResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DisplayName { get; set; } = string.Empty; // Human-readable location
    }
}