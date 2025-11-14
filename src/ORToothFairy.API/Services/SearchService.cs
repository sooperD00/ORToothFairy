using Microsoft.EntityFrameworkCore;
using ORToothFairy.API.Data;
using ORToothFairy.Core.Entities;
using ORToothFairy.Core.Models;

namespace ORToothFairy.API.Services;

public class SearchService
{
    private readonly ApplicationDbContext _context;

    public SearchService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PractitionerSearchResult>> SearchAsync(
        double userLat,
        double userLon,
        int? userSearchRadiusMiles = null)
    {
        // Get all active practitioners
        var practitioners = await _context.Practitioners
            .Where(p => p.IsActive)
            .Where(p => p.State == "OR") // Limit to Oregon for now
            .ToListAsync();

        // Calculate distances and filter based on user and practitioner preferences
        var results = practitioners
            .Select(p => new
            {
                Practitioner = p,
                Distance = CalculateDistance(userLat, userLon, p.Latitude, p.Longitude)
            })
            .Where(x => userSearchRadiusMiles == null || x.Distance <= userSearchRadiusMiles.Value)
            .Where(x => x.Practitioner.MaxTravelMiles == null || x.Distance <= x.Practitioner.MaxTravelMiles.Value)
            .OrderBy(x => x.Distance)
            .Select(x => new PractitionerSearchResult
            {
                PractitionerId = x.Practitioner.Id,
                FirstName = x.Practitioner.FirstName,
                LastName = x.Practitioner.LastName,
                Phone = x.Practitioner.Phone,
                Email = x.Practitioner.Email,
                AcceptsTexts = x.Practitioner.AcceptsTexts,
                AcceptsCalls = x.Practitioner.AcceptsCalls,
                UserPractitionerProximityMiles = x.Distance
            })
            .ToList();

        return results;
    }

    // Haversine formula: calculates distance between two lat/lon points
    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
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

    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}