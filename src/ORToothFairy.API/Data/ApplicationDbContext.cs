using Microsoft.EntityFrameworkCore;
using ORToothFairy.Core.Entities;

namespace ORToothFairy.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // This property becomes the "practitioners" table in PostgreSQL
    public DbSet<Practitioner> Practitioners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Index on lat/lon for faster distance queries
        modelBuilder.Entity<Practitioner>()
            .HasIndex(p => new { p.Latitude, p.Longitude });

        // Index on IsActive (we'll filter by this a lot)
        modelBuilder.Entity<Practitioner>()
            .HasIndex(p => p.IsActive);
    }
}
