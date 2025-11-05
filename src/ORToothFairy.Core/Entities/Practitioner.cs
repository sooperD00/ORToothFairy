namespace ORToothFairy.Core.Entities;

public class Practitioner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    // Geolocation (for distance searches)
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    // Business info
    public string? BusinessName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    
    // Services offered (JSON array for flexibility)
    public string Services { get; set; } = "[]";
    
    // Admin fields
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Future: Add UserId when we build auth
}
