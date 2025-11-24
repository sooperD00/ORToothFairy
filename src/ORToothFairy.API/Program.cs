using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ORToothFairy.API.Data;
using ORToothFairy.API.Repositories;
using ORToothFairy.Core.Repositories;
using ORToothFairy.Core.Services;

// This is a web app
var builder = WebApplication.CreateBuilder(args);

// CORS policy to allow MAUI app access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMAUI", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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
    await SeedData.SeedProfilePages(context);      // Add this BEFORE ClientProfiles because of FK
    await SeedData.SeedClientProfiles(context);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Make sure CORS is enabled
// add BEFORE app.UseHttpsRedirection();
// tighten to Azure-deployed app's URL on publish; `AllowAnyOrigin()` is fine for local dev
app.UseCors("AllowMAUI");  
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();