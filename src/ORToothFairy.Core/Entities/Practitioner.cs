namespace ORToothFairy.Core.Entities;

public class Practitioner
{
    // Primary key
    public int Id { get; set; }

    // Basic info
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Geolocation (for distance searches)
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Business info
    public string? PracticeName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string State { get; set; } = "OR";  // Default to Oregon (can change later)
    public string? ZipCode { get; set; }
    public string? Website { get; set; }  // NEW - useful for practitioners

    // Services offered (JSON array for flexibility)
    public string Services { get; set; } = "[]";

    // Admin fields
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }  // NEW - track modifications

    // Future: Add UserId when we build auth
}