4. Build MVP

- Actually code the thing
- Key people: Developer(s), designer if budget allows
- Output: Working app

# Phase 4: Build MVP - Implementation Log

**Purpose:** Capture what we actually built, key decisions, and lessons learned during MVP development.

---

## Milestone 1: Project Setup ✅ (Completed Nov 5, 2024)

### What We Built
- ✅ Created solution structure (`ORToothFairy.sln`)
- ✅ Set up three projects:
  - `ORToothFairy.Core` (business logic layer)
  - `ORToothFairy.API` (ASP.NET Core Web API)
  - `ORToothFairy.MAUI` (cross-platform app—Blazor Hybrid)
- ✅ Added EF Core + Npgsql NuGet packages
- ✅ Set up Railway PostgreSQL database
- ✅ Created initial migration (`InitialCreate`)
- ✅ Deployed "Hello World" API to Azure App Service

### Key Files Created
```
src/
├── ORToothFairy.Core/
│   └── Entities/              (empty—ready for Milestone 2)
├── ORToothFairy.API/
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── ApplicationDbContextFactory.cs  ← Needed for EF CLI tools
│   ├── appsettings.json       (connection string)
│   └── Program.cs             (DI setup)
└── ORToothFairy.MAUI/         (scaffolded, not modified yet)
```

### Key Decisions

**1. Why `ApplicationDbContextFactory`?**
- EF Core's `dotnet ef` CLI tools need to create `ApplicationDbContext` to run migrations
- Can't always infer from `Program.cs` automatically (especially with Railway connection strings)
- Factory gives explicit instructions: "Read `appsettings.json`, use Npgsql, connect to Railway"

**2. Why Railway (not local PostgreSQL)?**
- Easier to share with neighbor for testing
- Free tier sufficient for MVP
- Matches production setup (no "works on my machine" issues)

**3. Project structure (Core/API/MAUI separation)?**
- `Core` = Pure business logic (no dependencies on EF Core, ASP.NET, etc.)
- `API` = Infrastructure (database, HTTP, configuration)
- `MAUI` = UI layer (calls API, no direct database access)
- **Why:** Makes code testable, keeps concerns separated (good for portfolio)

### Lessons Learned

**1. Dependency Injection isn't optional in ASP.NET Core**
- The framework requires you to register services in `Program.cs`
- Without `builder.Services.AddDbContext<ApplicationDbContext>(...)`, the app can't create the context
- Constructor injection (`public MyController(ApplicationDbContext db)`) only works if DI is configured

**2. DbContext = Translator + Connection (not just schema)**
- It's the bridge between C# objects and SQL tables
- Contains: schema mapping (`DbSet<>` properties), connection info, LINQ→SQL translator
- Lives in `API/Data` (not `Core`) because it's infrastructure, not business logic

**3. Interfaces (`IDesignTimeDbContextFactory`) vs Injection**
- Both start with "I" but different concepts:
  - **Interface** = Contract (defines methods a class must implement)
  - **Injection** = How you get dependencies into constructors
- `IDesignTimeDbContextFactory<T>` = EF Core's interface for "how to create DbContext at design time"

**4. "Design time" vs "Runtime"**
- **Design time** = When YOU run `dotnet ef migrations add` (developer tools)
- **Runtime** = When USERS hit your API (production app running)
- Factory is only used at design time (EF CLI tools)

### Gotchas / Troubleshooting

**Problem:** `dotnet ef database update` failed with "Unable to create DbContext"  
**Solution:** Created `ApplicationDbContextFactory` to explicitly tell EF tools how to build the context

**Problem:** Confused about where DbContext belongs (Core vs API)  
**Solution:** DbContext is infrastructure (knows about PostgreSQL, connection strings) → goes in `API/Data`

### Time Spent
~10 hours (planned 8, extra time on learning DI concepts)

### Next Up
Milestone 2: Build `Practitioner` entity, seed test data, implement distance search logic

---

## Milestone 2: Core Search Logic ✅ (Completed Nov 14, 2024)

### What We Built
- ✅ Created `Practitioner` entity with geolocation fields
- ✅ Implemented repository pattern (IPractitionerRepository + implementation)
- ✅ Built SearchService with Haversine distance calculations
- ✅ Created GeocodingService supporting 3 input methods:
  - Direct lat/lon (device geolocation)
  - Zip code → lat/lon (Nominatim API)
  - Full address → lat/lon (Nominatim API)
- ✅ Built SearchController REST API endpoint
- ✅ Seeded 10 test practitioners
- ✅ Wrote 32 comprehensive tests (all passing)

### Key Files Created/Modified
```
src/
├── ORToothFairy.Core/
│   ├── Entities/
│   │   └── Practitioner.cs           (domain entity)
│   ├── Models/
│   │   └── PractitionerSearchResult.cs (DTO for search results)
│   ├── Services/
│   │   ├── ISearchService.cs          (interface)
│   │   ├── SearchService.cs           (business logic)
│   │   ├── IGeocodingService.cs       (interface)
│   │   └── GeocodingService.cs        (Nominatim integration)
│   └── Repositories/
│       └── IPractitionerRepository.cs (data access contract)
├── ORToothFairy.API/
│   ├── Controllers/
│   │   └── SearchController.cs       (REST endpoint)
│   ├── Repositories/
│   │   └── PractitionerRepository.cs (EF Core implementation)
│   ├── Data/
│   │   ├── ApplicationDbContext.cs   (updated with Services JSONB config)
│   │   └── SeedData.cs               (10 test practitioners)
│   └── Program.cs                    (DI registration)
└── tests/
    └── ORToothFairy.Tests/
        ├── Controllers/
        │   └── SearchControllerTests.cs (15 tests)
        └── API/Services/
            └── SearchServiceTests.cs     (10 tests)
```

### Key Decisions

**1. Repository Pattern (Clean Architecture)**
- **Problem:** SearchService needed database access, but Core shouldn't depend on EF Core
- **Solution:** Created `IPractitionerRepository` interface in Core, implemented in API
- **Why:** Makes Core testable without database, separates concerns properly
- **Trade-off:** Extra abstraction layer, but worth it for portfolio quality

**2. Services Field Storage (JSON vs separate table)**
- **Decision:** Store as PostgreSQL JSONB with EF Core automatic serialization
- **In C#:** `List<string> Services { get; set; }`
- **In DB:** JSONB array `["Cleanings", "Sealants"]`
- **Why:** Flexible schema, handles commas/special chars, no join queries needed
- **Implementation:** EF Core `HasConversion()` in `OnModelCreating`

**3. Geocoding Provider (Nominatim)**
- **Alternatives considered:** Google Maps API, Mapbox, Azure Maps
- **Chose Nominatim (OpenStreetMap):** Free, no API key, good for MVP
- **Trade-offs:** Rate limited (1 req/sec), less accurate than Google, but sufficient
- **Future:** Can swap to paid service if needed (abstracted behind IGeocodingService)

**4. Distance Calculation (Haversine Formula)**
- **Alternatives:** PostGIS `ST_Distance`, simple Pythagorean approximation
- **Chose Haversine:** Accurate for Earth's curvature, no PostGIS extension required
- **Performance:** Fast enough for <1000 practitioners (in-memory calculation after DB fetch)
- **Future:** Move to PostGIS if we scale beyond Oregon

**5. Test Strategy (Mocking vs Integration)**
- **SearchServiceTests:** Mock repository (no database) - tests business logic
- **SearchControllerTests:** Mock both services - tests HTTP layer only
- **Trade-off:** Fast tests, but need integration tests later for full E2E coverage
- **32 tests cover:** Happy paths (6), error cases (6), edge cases (3), business logic (10+)

### Architecture Pattern We Landed On

**Clean Architecture (Onion/Hexagonal):**
```
┌─────────────────────────────────────┐
│  Core (Business Logic)              │
│  - Entities (Practitioner)          │
│  - Interfaces (ISearchService)      │
│  - Business Logic (SearchService)   │
│  - NO infrastructure dependencies   │
└─────────────────────────────────────┘
              ↑ depends on
              │
┌─────────────────────────────────────┐
│  API (Infrastructure)               │
│  - Controllers (HTTP)               │
│  - Repositories (EF Core)           │
│  - DbContext, migrations            │
│  - Implements Core interfaces       │
└─────────────────────────────────────┘
```

**Why this matters for portfolio:**
- Shows understanding of SOLID principles (Dependency Inversion)
- Testable without infrastructure
- Can swap implementations (e.g., different database)
- Industry-standard pattern at mature companies

### Lessons Learned

**1. TDD Methodology (Write Tests First)**
- Started writing controller before tests → got confused about responsibilities
- **Better approach:** Write test cases first, then implement to make tests pass
- Tests document expected behavior better than comments
- Caught edge cases early (e.g., both zipCode AND address provided)

**2. DTOs vs Entities (When to Use Each)**
- **Entity (`Practitioner`):** Database representation, all fields
- **DTO (`PractitionerSearchResult`):** API response, only relevant fields + calculated distance
- **Why separate:** Don't expose internal DB structure, API can evolve independently
- **Example:** Entity has `MaxTravelMiles`, but DTO doesn't (user doesn't care)

**3. Mocking Strategies**
- Mock at service boundaries (ISearchService, IGeocodingService), not DbContext
- Makes tests cleaner and faster
- Don't mock what you own (e.g., don't mock Practitioner class)

**4. API Design Patterns**
- **Query params for filtering:** `?latitude=45.5&distanceMiles=10` (REST convention)
- **Wrap responses in metadata object:** `{ results: [...], count: 5, searchLocation: "..." }`
- **Error responses use 400 Bad Request:** Clear error messages in JSON

**5. C# Language Features We Used**
- **Nullable reference types:** `string?` (can be null) vs `string` (never null)
- **Expression-bodied members:** `public bool IsActive => true;`
- **Collection initializers:** `new List<string> { "A", "B" }`
- **LINQ:** `.Where().Select().OrderBy()` for data transformation
- **Async/await:** All service methods async for I/O operations

### Gotchas / Troubleshooting

**Problem:** Tests failing with "cannot convert ApplicationDbContext to IPractitionerRepository"  
**Solution:** Refactored SearchService to depend on repository interface, not DbContext directly

**Problem:** `Services` field had red squiggles in Core project  
**Solution:** Core doesn't reference EF Core - changed from `string` (JSON) to `List<string>` with converter in API

**Problem:** Distance calculation test expected ~10 miles, actually ~6.4 miles  
**Solution:** Verified with real-world coordinates (Portland to Beaverton), updated test assertion

**Problem:** Forgot to add `Services` property to `PractitionerSearchResult` DTO  
**Solution:** Added property, updated SearchService to map it from entity

### API Endpoints Created

**Base URL:** `https://localhost:5001/api/search` (local) or Azure URL (production)

**Search by geolocation:**
```
GET /api/search?latitude=45.5&longitude=-122.6&distanceMiles=10
```

**Search by zip code:**
```
GET /api/search?zipCode=97006&distanceMiles=20
```

**Search by address:**
```
GET /api/search?address=123 Main St, Portland OR
```

**Response format:**
```json
{
  "results": [
    {
      "practitionerId": 1,
      "firstName": "Jane",
      "lastName": "Smith",
      "userPractitionerProximityMiles": 2.3,
      "phone": "503-555-1234",
      "email": "jane@example.com",
      "acceptsCalls": true,
      "acceptsTexts": false,
      "services": ["Cleanings", "Sealants"]
    }
  ],
  "count": 1,
  "searchLocation": "Beaverton, OR 97006",
  "distanceFilter": 10
}
```

### Test Coverage Summary

**32 tests, 100% passing:**

**SearchControllerTests (15 tests):**
- Happy paths: lat/lon search, zip search, address search, distance filters
- Error cases: no location, invalid lat/lon, bad zip, bad address, invalid distance
- Edge cases: multiple inputs (priority handling), lat/lon bounds

**SearchServiceTests (10 tests):**
- Distance calculations (Haversine accuracy)
- Sorting (nearest first)
- Filtering (user radius + practitioner MaxTravelMiles)
- Edge cases (identical location, zero distance filter)

**PractitionerTests (7 tests):**
- Entity validation, default values, property assignments

### Performance Considerations

**Current approach (fetch all, filter in memory):**
- ✅ Simple implementation
- ✅ Fast for <1000 practitioners
- ❌ Won't scale to 10k+ practitioners

**Future optimization (if needed):**
- PostGIS extension with spatial indexes
- `ST_Distance` in SQL query (database does filtering)
- Pagination for large result sets

**For MVP:** Current approach is fine (400 max practitioners in Oregon)

### Time Spent
~4 hours actual coding (planned 20 hours in original estimate)
- Included: Full refactor to clean architecture, comprehensive tests, geocoding integration
- Learning moments: Repository pattern, mocking strategies, TDD workflow
- Efficiency gains: Paired programming with Claude, clear requirements from Phase 2

### What's Next
**Milestone 3:** Frontend Search UI (MAUI Blazor)
- Build search page with 3 input methods
- Display results list
- Call Search API from app
