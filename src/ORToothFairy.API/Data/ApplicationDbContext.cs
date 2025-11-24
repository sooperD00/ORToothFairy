using Microsoft.EntityFrameworkCore;
using ORToothFairy.Core.Entities;
using System.Text.Json;

namespace ORToothFairy.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // This property becomes the "practitioners" or "clientprofiles" table in PostgreSQL
    // virtual keyword lets Moq override this in tests
    public virtual DbSet<Practitioner> Practitioners { get; set; }
    public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
    public virtual DbSet<ProfilePage> ProfilePages { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Indexes and configurations for Practitioner
        modelBuilder.Entity<Practitioner>(entity =>
        {
            // Index on lat/lon for faster distance queries
            entity.HasIndex(p => new { p.Latitude, p.Longitude });

            // Index on IsActive (we'll filter by this a lot)
            entity.HasIndex(p => p.IsActive);

            // Configure Services as JSONB column with automatic serialization
            entity.Property(p => p.Services)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!) ?? new List<string>()
                );
        });

        // Indexes for ClientProfile
        modelBuilder.Entity<ClientProfile>(entity =>
        {
            entity.HasIndex(cp => cp.IsActive);
            entity.HasIndex(cp => new { cp.ProfilePageId, cp.DisplayOrder });
        });

        // Indexes and relationships for ProfilePage
        modelBuilder.Entity<ProfilePage>(entity =>
        {
            entity.HasIndex(pp => pp.IsActive);
            entity.HasIndex(pp => pp.DisplayOrder);

            // Configure the one-to-many relationship
            entity.HasMany(pp => pp.ClientProfiles)
                .WithOne()
                .HasForeignKey(cp => cp.ProfilePageId)
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete profiles if page is deleted
        });
    }
}
