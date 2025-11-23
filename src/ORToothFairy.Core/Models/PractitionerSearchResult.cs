namespace ORToothFairy.Core.Models;

public class PractitionerSearchResult
{
    // For the Practitioner "card"
    public int PractitionerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool AcceptsTexts { get; set; }
    public bool AcceptsCalls { get; set; }
    public string? Email { get; set; }
    public double UserPractitionerProximityMiles { get; set; }
    public List<string> Services { get; set; } = new();

    // For maps functionality
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; } = string.Empty;  // For error fallback
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}