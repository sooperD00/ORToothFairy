namespace ORToothFairy.Core.Models;

public class PractitionerSearchResult
{
    public int PractitionerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool AcceptsTexts { get; set; }
    public bool AcceptsCalls { get; set; }
    public string? Email { get; set; }
    public double UserPractitionerProximityMiles { get; set; }
}