using Microsoft.EntityFrameworkCore;
using ORToothFairy.API.Data;
using ORToothFairy.Core.Entities;
using ORToothFairy.Core.Repositories;

namespace ORToothFairy.API.Repositories;

/// <summary>
/// EF Core implementation of practitioner repository
/// This is the "adapter" between Core's interface and our database
/// </summary>
public class PractitionerRepository : IPractitionerRepository
{
    private readonly ApplicationDbContext _context;

    public PractitionerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Practitioner>> GetActivePractitionersAsync(string state)
    {
        return await _context.Practitioners
            .Where(p => p.IsActive)
            .Where(p => p.State == state)
            .ToListAsync();
    }
}