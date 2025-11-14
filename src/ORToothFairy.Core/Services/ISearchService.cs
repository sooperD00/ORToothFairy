using ORToothFairy.Core.Models;

namespace ORToothFairy.Core.Services;

/// <summary>
/// Service for searching EDHS practitioners by location
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// Search for practitioners near a given location
    /// </summary>
    /// <param name="userLat">User's latitude</param>
    /// <param name="userLon">User's longitude</param>
    /// <param name="userSearchRadiusMiles">Optional distance filter (5, 10, 20 miles, or null for all)</param>
    /// <returns>List of practitioners sorted by distance</returns>
    Task<List<PractitionerSearchResult>> SearchAsync(
        double userLat,
        double userLon,
        int? userSearchRadiusMiles = null);
}