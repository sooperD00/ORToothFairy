using Microsoft.EntityFrameworkCore;
using ORToothFairy.Core.Entities;
using System.Collections.Generic;

namespace ORToothFairy.API.Data;

public static class SeedData
{
    public static async Task SeedPractitioners(ApplicationDbContext context)
    {
        // Check if we already have practitioners (don't seed twice)
        var count = await context.Practitioners.CountAsync();
        Console.WriteLine($"Found {count} practitioners in database");
        if (count > 0)
        {
            Console.WriteLine("Database already seeded. Skipping...");
            return;
        }

        Console.WriteLine("Seeding practitioners...");

        var practitioners = new List<Practitioner>
        {
            // 1. Irene Newman - Historical figure, OUT OF STATE (test control)
            new Practitioner
            {
                FirstName = "Irene",
                LastName = "Newman",
                Email = "irene.newman@example.com",
                Phone = "203-555-1906",
                Latitude = 41.3083,
                Longitude = -73.0552,
                Address = "123 Prevention St",
                City = "Bridgeport",
                State = "CT",
                ZipCode = "06604",
                PracticeName = "Fones School of Dental Hygiene",
                Website = "https://www.qu.edu/fones",
                Services = new List<string> { "Oral Prophylaxis", "Oral Hygiene Education", "Preventive Care" },
                IsActive = true
            },

            // 2. Esther Wilkins - Book author, Hillsboro (our area!)
            new Practitioner
            {
                FirstName = "Esther",
                LastName = "Wilkins",
                Email = "esther.wilkins@example.com",
                Phone = "503-555-1959",
                Latitude = 45.5365,
                Longitude = -122.8650,
                Address = "2850 NE Brookwood Pkwy",
                City = "Hillsboro",
                State = "OR",
                ZipCode = "97124",
                PracticeName = "Wilkins Dental Hygiene",
                Website = "https://hillsboro-oregon.gov/library",
                Services = new List<string> { "Clinical Practice", "Evidence-Based Care", "Patient Education" },
                IsActive = true
            },

            // 3. Dr. Herbert C. Miller - OHSU founder, Portland
            new Practitioner
            {
                FirstName = "Herbert",
                LastName = "Miller",
                Email = "herbert.miller@example.com",
                Phone = "503-555-1898",
                Latitude = 45.4993,
                Longitude = -122.6739,
                Address = "2730 SW Moody Ave",
                City = "Portland",
                State = "OR",
                ZipCode = "97201",
                PracticeName = "OHSU Dental Clinic",
                Website = "https://www.ohsu.edu/school-of-dentistry",
                Services = new List<string> { "General Dentistry", "Dental Education", "Community Outreach" },
                IsActive = true
            },

            // 4. Dr. Theodor Geisel (Dr. Seuss) - Children's book author, Eugene
            new Practitioner
            {
                FirstName = "Theodor",
                LastName = "Geisel",
                Email = "dr.seuss@example.com",
                Phone = "541-555-1937",
                Latitude = 44.0521,
                Longitude = -123.0868,
                Address = "100 W 10th Ave",
                City = "Eugene",
                State = "OR",
                ZipCode = "97401",
                PracticeName = "The Tooth Book Dental Hygiene",
                Website = "https://www.seussville.com",
                Services = new List<string> { "Pediatric Dental Care", "Children's Oral Health Education", "Preventive Care" },
                IsActive = true
            },

            // 5. Laura Numeroff - Children's book author, Salem
            new Practitioner
            {
                FirstName = "Laura",
                LastName = "Numeroff",
                Email = "laura.numeroff@example.com",
                Phone = "503-555-1985",
                Latitude = 44.9391,
                Longitude = -123.0331,
                Address = "585 Liberty St SE",
                City = "Salem",
                State = "OR",
                ZipCode = "97301",
                PracticeName = "If You Brush Your Teeth Dental Clinic",
                Website = "https://www.lauranumeroff.com",
                Services = new List<string> { "Pediatric Cleanings", "Cavity Prevention", "Nutritional Counseling" },
                IsActive = true
            },

            // 6. Naomi Petrie - First Oregon DHAT, Coos Bay (Native American representation)
            new Practitioner
            {
                FirstName = "Naomi",
                LastName = "Petrie",
                Email = "naomi.petrie@example.com",
                Phone = "541-555-2017",
                Latitude = 43.3665,
                Longitude = -124.2179,
                Address = "1245 Tribal Way",
                City = "Coos Bay",
                State = "OR",
                ZipCode = "97420",
                PracticeName = "Petrie Tribal Dental Services",
                Website = "https://www.ctclusi.org",
                Services = new List<string> { "Preventive Care", "Basic Restorations", "Community Health" },
                IsActive = true
            },

            // 7. Dr. E.H. Griffen - Oregon Trail dentist, Oregon City (EASTER EGG!)
            new Practitioner
            {
                FirstName = "E.H.",
                LastName = "Griffen",
                Email = "eh.griffen@example.com",
                Phone = "503-555-1850",
                Latitude = 45.3573,
                Longitude = -122.6068,
                Address = "123 Wagon Trail Rd",
                City = "Oregon City",
                State = "OR",
                ZipCode = "97045",
                PracticeName = "You Have Died of Dysentery Dental Clinic",
                Website = null,  // Pioneer dentist - no website!
                Services = new List<string> { "Emergency Extractions", "Pioneer Dental Care", "Gold Fillings" },
                IsActive = true
            },

            // 8. Dr. Sacket - First Oregon dentist, Astoria (Coast)
            new Practitioner
            {
                FirstName = "Dr.",
                LastName = "Sacket",
                Email = "dr.sacket@example.com",
                Phone = "503-555-1847",
                Latitude = 46.1879,
                Longitude = -123.8313,
                Address = "450 10th Street",
                City = "Astoria",
                State = "OR",
                ZipCode = "97103",
                PracticeName = "Sacket Coastal Dental",
                Website = null,  // 1847 - definitely no website!
                Services = new List<string> { "Coastal Community Care", "Emergency Services", "General Dentistry" },
                IsActive = true
            },

            // 9. Frieda Atherton Pickett - Textbook author, Bend (Central Oregon)
            new Practitioner
            {
                FirstName = "Frieda",
                LastName = "Pickett",
                Email = "frieda.pickett@example.com",
                Phone = "541-555-1973",
                Latitude = 44.0582,
                Longitude = -121.3153,
                Address = "601 NW Wall St",
                City = "Bend",
                State = "OR",
                ZipCode = "97703",
                PracticeName = "Pickett Hygiene Education Center",
                Website = "https://www.deschuteslibrary.org",
                Services = new List<string> { "Clinical Instruction", "Advanced Techniques", "Continuing Education" },
                IsActive = true
            },

            // 10. Dr. Alfred Fones - Founder/mentor, Pendleton (Eastern Oregon)
            new Practitioner
            {
                FirstName = "Alfred",
                LastName = "Fones",
                Email = "alfred.fones@example.com",
                Phone = "541-555-1913",
                Latitude = 45.6721,
                Longitude = -118.7886,
                Address = "502 SW Dorion Ave",
                City = "Pendleton",
                State = "OR",
                ZipCode = "97801",
                PracticeName = "Fones Prevention Clinic",
                Website = "https://www.qu.edu/fones",
                Services = new List<string> { "Preventive Education", "Hygienist Training", "Community Outreach" },
                IsActive = true
            }
        };

        await context.Practitioners.AddRangeAsync(practitioners);
        await context.SaveChangesAsync();

        Console.WriteLine($"[SUCCESS] Seeded {practitioners.Count} practitioners!");
    }

    public static async Task SeedClientProfiles(ApplicationDbContext context)
    {
        // Check if we already have client profiles (don't seed twice)
        var count = await context.ClientProfiles.CountAsync();
        Console.WriteLine($"Found {count} client profiles in database");
        if (count > 0)
        {
            Console.WriteLine("Client profiles already seeded. Skipping...");
            return;
        }

        Console.WriteLine("Seeding client profiles...");
        var clientProfiles = new List<ClientProfile>
    {
        // Myself or Family
        new ClientProfile
        {
            Id = 1,
            Name = "MyselfOrFamily",
            Headline = "I need care for myself or my family",
            ExpandedDescription = "You can connect with a dental hygienist at a doctor's office, another care facility, or contact one that comes to your home for mobile services.",
            CardColor = "#E3F2FD",
            HighlightColor = "#BBDEFB",
            DefaultSearchType = "Geolocation",
            ShowRadiusOption = true,
            DefaultRadiusMiles = 25,
            PageCategory = "IndividualsAndFamilies",
            DisplayOrder = 1,
            IsActive = true,
            UserStoryIds = "US-001,US-002"
        },

        // Care Facility (Patient/Guardian)
        new ClientProfile
        {
            Id = 2,
            Name = "FamilyMemberInCareFacility",
            Headline = "I need care for a family member in a nursing home or care facility",
            ExpandedDescription = "Many dental hygienists provide services at care facilities including assisted living, memory care, nursing homes, and other residential settings. You can search for hygienists who visit your loved one's facility or contact facilities directly to ask about their dental hygiene services.",
            CardColor = "#F3E5F5",
            HighlightColor = "#E1BEE7",
            DefaultSearchType = "Address",
            ShowRadiusOption = true,
            DefaultRadiusMiles = 10,
            PageCategory = "IndividualsAndFamilies",
            DisplayOrder = 2,
            IsActive = true,
            UserStoryIds = "US-003,US-004"
        },

        // Help Needed (Financial/Social)
        new ClientProfile
        {
            Id = 3,
            Name = "NeedHelpGettingCare",
            Headline = "I need help getting care",
            ExpandedDescription = "Dental hygiene services are often covered by Medicare/Medicaid or other assistance programs. You can search for dental hygienists who accept these programs, work in community health centers, or provide services in underserved areas.",
            CardColor = "#FFF3E0",
            HighlightColor = "#FFE0B2",
            DefaultSearchType = "Zip",
            ShowRadiusOption = true,
            DefaultRadiusMiles = 50,
            PageCategory = "IndividualsAndFamilies",
            DisplayOrder = 1,
            IsActive = true,
            UserStoryIds = "US-005,US-006,US-007"
        },

        // Care Facility (B2B)
        new ClientProfile
        {
            Id = 4,
            Name = "CareFacility",
            Headline = "I run a care facility",
            ExpandedDescription = "Connect with dental hygienists who provide on-site services at assisted living facilities, nursing homes, memory care centers, and other residential care settings. Many hygienists specialize in serving facilities and can establish regular visit schedules for your residents.",
            CardColor = "#E8F5E9",
            HighlightColor = "#C8E6C9",
            DefaultSearchType = "Address",
            ShowRadiusOption = true,
            DefaultRadiusMiles = 25,
            PageCategory = "B2B",
            DisplayOrder = 1,
            IsActive = true,
            UserStoryIds = "US-008,US-009"
        },

        // Dentist Office (B2B)
        new ClientProfile
        {
            Id = 5,
            Name = "DentistOffice",
            Headline = "I own a dentist office",
            ExpandedDescription = "Find dental hygienists available for contract work, part-time positions, or temp coverage. You can search by location and filter by availability, certifications, and specializations to find the right fit for your practice.",
            CardColor = "#E1F5FE",
            HighlightColor = "#B3E5FC",
            DefaultSearchType = "Geolocation",
            ShowRadiusOption = true,
            DefaultRadiusMiles = 15,
            PageCategory = "B2B",
            DisplayOrder = 2,
            IsActive = true,
            UserStoryIds = "US-010,US-011"
        },

        // Other (B2B)
        new ClientProfile
        {
            Id = 6,
            Name = "ContactUs",
            Headline = "I need something different",
            ExpandedDescription = "Have a unique situation or specific need? Contact us directly and we'll help you connect with the right dental hygiene services.",
            CardColor = "#F5F5F5",
            HighlightColor = "#EEEEEE",
            DefaultSearchType = "Contact",
            ShowRadiusOption = false,
            DefaultRadiusMiles = 0,
            PageCategory = "B2B",
            DisplayOrder = 3,
            IsActive = true,
            UserStoryIds = "US-012"
        }


    };

        await context.ClientProfiles.AddRangeAsync(clientProfiles);
        await context.SaveChangesAsync();
        Console.WriteLine($"Successfully seeded {clientProfiles.Count} client profiles");
    }

    public static async Task SeedProfilePages(ApplicationDbContext context)
    {
        // Check if we already have profile pages (don't seed twice)
        var count = await context.ProfilePages.CountAsync();
        Console.WriteLine($"Found {count} profile pages in database");
        if (count > 0)
        {
            Console.WriteLine("Profile pages already seeded. Skipping...");
            return;
        }

        Console.WriteLine("Seeding profile pages...");
        var profilePages = new List<ProfilePage>
    {
        // Page: Individuals & Families
        new ProfilePage
        {
            Id = 1,
            Name = "IndividualsAndFamilies",
            DisplayName = "Individuals & Families",
            StockPhotoUrl = "images/nathan-anderson-GM5Yn5XRVqA-unsplash.jpg", // Update with your actual path
            CardColor = "rgba(255, 255, 255, 0)", // Transparent
            DisplayOrder = 1,
            IsActive = true
        },
        // Page: Business & Facilities
        new ProfilePage
        {
            Id = 2,
            Name = "BusinessAndFacilities",
            DisplayName = "Business & Facilities",
            StockPhotoUrl = "images/national-cancer-institute-1c8sj2IO2I4-unsplash.jpg", // Update with your actual path
            CardColor = "rgba(255, 255, 255, 0)", // Transparent
            DisplayOrder = 2,
            IsActive = true
        }
    };

        await context.ProfilePages.AddRangeAsync(profilePages);
        await context.SaveChangesAsync();
        Console.WriteLine($"Successfully seeded {profilePages.Count} profile pages");
    }
}