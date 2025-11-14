using Microsoft.EntityFrameworkCore;
using ORToothFairy.Core.Entities;
using System.Collections.Generic;

namespace ORToothFairy.API.Data;

public static class SeedData
{
    public static async Task SeedPractitioners(ApplicationDbContext context)
    {
        // Check if we already have practitioners (don't seed twice)
        if (await context.Practitioners.AnyAsync())
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
}