using ORToothFairy.Core.Entities;
using Xunit;

namespace ORToothFairy.Tests.Core.Entities;

public class PractitionerTests
{
    [Fact]
    public void Practitioner_Should_Have_Default_Values()
    {
        // Arrange & Act
        var practitioner = new Practitioner();

        // Assert
        Assert.Equal(0, practitioner.Id);
        Assert.Equal(string.Empty, practitioner.FirstName);
        Assert.Equal(string.Empty, practitioner.Email);
        Assert.Equal("[]", practitioner.Services);
        Assert.Equal("OR", practitioner.State);  // Default to Oregon
        Assert.True(practitioner.IsActive);
        Assert.NotEqual(DateTime.MinValue, practitioner.CreatedAt);
    }

    [Fact]
    public void Practitioner_IreneNewman_Should_Allow_Setting_Properties()
    {
        // Arrange
        // Irene Newman - World's first dental hygienist (1906)
        var practitioner = new Practitioner
        {
            FirstName = "Irene",
            LastName = "Newman",
            Email = "irene.newman@example.com",
            Phone = "203-555-1906",  // 1906 = year she became first dental hygienist
            Latitude = 41.3083,      // Bridgeport, CT
            Longitude = -73.0552,
            Address = "123 Prevention St",
            City = "Bridgeport",
            State = "CT",  // Not Oregon - useful for filtering tests later
            ZipCode = "06604",
            Services = "[\"Oral Prophylaxis\", \"Oral Hygiene Education\", \"Preventive Care\"]",
            PracticeName = "Fones School of Dental Hygiene"
        };

        // Act & Assert
        Assert.Equal("Irene", practitioner.FirstName);
        Assert.Equal("Newman", practitioner.LastName);
        Assert.Equal("irene.newman@example.com", practitioner.Email);
        Assert.Equal(41.3083, practitioner.Latitude);
        Assert.Equal("Bridgeport", practitioner.City);
        Assert.Equal("CT", practitioner.State);  // Explicitly not Oregon
        Assert.Equal("Fones School of Dental Hygiene", practitioner.PracticeName);
    }

    [Fact]
    public void Practitioner_EstherWilkins_Should_Set_Oregon_Location()
    {
        // Arrange
        // Esther Wilkins - "Mother of Modern Dental Hygiene" (1916-2016)
        // Placing her in Portland, OR for our Oregon-focused app
        var practitioner = new Practitioner
        {
            FirstName = "Esther",
            LastName = "Wilkins",
            Email = "esther.wilkins@example.com",
            Phone = "503-555-1959",  // 1959 = year her textbook was published
            Latitude = 45.5152,      // Portland, OR
            Longitude = -122.6784,
            Address = "456 Evidence-Based Blvd",
            City = "Portland",
            State = "OR",
            ZipCode = "97201",
            Services = "[\"Clinical Practice\", \"Evidence-Based Care\", \"Patient Education\"]",
            PracticeName = "Wilkins Dental Hygiene"
        };

        // Act & Assert
        Assert.Equal("Esther", practitioner.FirstName);
        Assert.Equal("OR", practitioner.State);  // Oregon practitioner
        Assert.Equal(45.5152, practitioner.Latitude);
        Assert.Equal("Portland", practitioner.City);
    }
}