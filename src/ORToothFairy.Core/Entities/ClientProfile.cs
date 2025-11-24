namespace ORToothFairy.Core.Entities;

/// <summary>
/// Represents a client profile category shown on the landing page.
/// Each profile defines the UX for a specific user journey (individuals, B2B, need help, etc.)
/// </summary>
public class ClientProfile
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty; // "DentistOffice", "MyselfOrFamily"

    // Display content
    public string Headline { get; set; } = string.Empty; // "I own a Dentist Office", "I need oral healthcare for myself..."
    public string ExpandedDescription { get; set; } = string.Empty; // Full explanation on accordion expand

    // Visual styling
    public string CardColor { get; set; } = string.Empty; // Hex color for card background
    public string HighlightColor { get; set; } = string.Empty; // Hex color for search suggestion box

    // Search behavior defaults (simple approach - go JSON if this grows to 8+ fields)
    public string DefaultSearchType { get; set; } = "Geolocation"; // "Geolocation", "Address", "Zip"
    public bool ShowRadiusOption { get; set; } = true;
    public int DefaultRadiusMiles { get; set; } = 25; // null or 5, 10, 25, etc.

    // Organization & filtering
    public string PageCategory { get; set; } = string.Empty; // "IndividualsAndFamilies", "B2B"
    public int DisplayOrder { get; set; } = 0; // Sort order within page category
    public bool IsActive { get; set; } = false; // Toggle visibility for demos

    // Documentation mapping (keeping it simple for portfolio purposes)
    public string UserStoryIds { get; set; } = string.Empty; // Comma-delimited: "US-001,US-002"

    // Relationships
    public int ProfilePageId { get; set; } // FK to ProfilePage (parent category)

    // Proper relational approach when you're ready for it:
    // public ICollection<ClientProfileUserStory> UserStories { get; set; } = new List<ClientProfileUserStory>();
}