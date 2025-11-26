using ORToothFairy.Core.Entities;
using ORToothFairy.Core.Models;
using ORToothFairy.Core.Repositories;

namespace ORToothFairy.Core.Services;

/// <summary>
/// Implementation of search service for finding EDHS practitioners
/// </summary>
public class SearchService : ISearchService
{
    private readonly IPractitionerRepository _repository;

    public SearchService(IPractitionerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PractitionerSearchResult>> SearchAsync(
        double userLat,
        double userLon,
        int? userSearchRadiusMiles = null)
    {
        Console.WriteLine($"[SearchService] Received search request:");
        Console.WriteLine($"  User location: ({userLat}, {userLon})");
        Console.WriteLine($"  Distance filter: {userSearchRadiusMiles?.ToString() ?? "NULL (show all)"}");

        // Get all active practitioners from repository
        // (Repository handles the database query - Core doesn't care how)
        // Limit to "OR" state for this implementation
        var practitioners = await _repository.GetActivePractitionersAsync("OR");

        Console.WriteLine($"[SearchService] Found {practitioners.Count} practitioners in OR");

        // Calculate distances and filter based on user and practitioner preferences
        var results = practitioners
            .Select(p => new
            {
                Practitioner = p,
                Distance = CalculateDistance(userLat, userLon, p.Latitude, p.Longitude)
            })
            .Where(x => {
                var passesDistanceFilter = userSearchRadiusMiles == null || x.Distance <= userSearchRadiusMiles.Value;
                Console.WriteLine($"  {x.Practitioner.FirstName} {x.Practitioner.LastName}: {x.Distance:F1} miles - Filter: {passesDistanceFilter}");
                return passesDistanceFilter;
            })
            .Where(x => x.Practitioner.MaxTravelMiles == null || x.Distance <= x.Practitioner.MaxTravelMiles.Value)
            .OrderBy(x => x.Distance)
            .Select(x => new PractitionerSearchResult
            {
                // Practitioner "card"
                PractitionerId = x.Practitioner.Id,
                FirstName = x.Practitioner.FirstName,
                LastName = x.Practitioner.LastName,
                Phone = x.Practitioner.Phone,
                Email = x.Practitioner.Email,
                AcceptsTexts = x.Practitioner.AcceptsTexts,
                AcceptsCalls = x.Practitioner.AcceptsCalls,
                UserPractitionerProximityMiles = x.Distance,
                Services = x.Practitioner.Services,

                // For Map Directions
                Latitude = x.Practitioner.Latitude,
                Longitude = x.Practitioner.Longitude,
                Address = x.Practitioner.Address,
                City = x.Practitioner.City,
                ZipCode = x.Practitioner.ZipCode
            })
            .ToList();

        return results;
    }

    // Haversine formula: calculates distance between two lat/lon points
    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadiusMiles = 3959.0; // Earth's radius in miles

        // Convert degrees to radians
        var dLat = DegreesToRadians(lat2 - lat1);
        var dLon = DegreesToRadians(lon2 - lon1);
        lat1 = DegreesToRadians(lat1);
        lat2 = DegreesToRadians(lat2);

        // Haversine formula
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadiusMiles * c;
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}