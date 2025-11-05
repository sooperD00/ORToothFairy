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

## Milestone 2: Core Search Logic (In Progress)

(Will fill this out as we go...)