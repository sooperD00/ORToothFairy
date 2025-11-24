using ORToothFairy.Core.Entities;


/// <summary>
/// Represents a top-level category page shown on the landing page (e.g., "Individuals & Families", "Business & Facilities").
/// Each ProfilePage contains multiple ClientProfiles that appear as accordion cards when the page card is clicked.
/// This structure allows for flexible demo configurations - new pages and profiles can be toggled on/off 
/// and reordered without code changes, making it easy to respond quickly to stakeholder feedback.
/// </summary>
public class ProfilePage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // "IndividualsAndFamilies", "B2B"
    public string DisplayName { get; set; } = string.Empty; // "Individuals & Families", "Business & Facilities"
    public string StockPhotoUrl { get; set; } = string.Empty;
    public string CardColor { get; set; } = string.Empty; // Can be transparent via rgba
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation property
    public ICollection<ClientProfile> ClientProfiles { get; set; } = new List<ClientProfile>();
}