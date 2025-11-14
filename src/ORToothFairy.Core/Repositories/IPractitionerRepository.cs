using ORToothFairy.Core.Entities;

namespace ORToothFairy.Core.Repositories;

/// <summary>
/// Repository for accessing practitioner data
/// Core defines the contract, API implements it using EF Core
/// This keeps Core independent of database/infrastructure concerns
/// </summary>
public interface IPractitionerRepository
{
    /// <summary>
    /// Get all active practitioners in a given state
    /// </summary>
    /// <param name="state">State abbreviation (e.g., "OR")</param>
    /// <returns>List of active practitioners</returns>
    Task<List<Practitioner>> GetActivePractitionersAsync(string state);
}