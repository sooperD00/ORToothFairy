using Microsoft.EntityFrameworkCore;
using ORToothFairy.API.Data;
using ORToothFairy.API.Repositories;
using ORToothFairy.Core.Repositories;
using ORToothFairy.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpClient for geocoding service
builder.Services.AddHttpClient<IGeocodingService, GeocodingService>();

// Register repository (API implements Core's interface)
builder.Services.AddScoped<IPractitionerRepository, PractitionerRepository>();

// SearchService depends on IPractitionerRepository
builder.Services.AddScoped<ISearchService, SearchService>();

// Geocoding service
builder.Services.AddHttpClient<IGeocodingService, GeocodingService>();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seed the database (only runs once)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedData.SeedPractitioners(context);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();