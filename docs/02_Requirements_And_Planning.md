# Requirements & Planning Phase - EDHS Finder Project -> OR Tooth Fairy Project

**Phase Status:** In Progress  
**Previous Phase:** [Discovery](01_Discovery.md) ✅  
**Next Phase:** [Legal & Business Setup](03_Legal_Business_Setup.md)

---

## Product Requirements

### User Stories (MVP)

#### As a Patient
- I want to search for EDHS practitioners near me so I can get dental care
- I want to search by my current location, zip code, or address so I have flexibility
- I want to filter results by distance (5/10/20 miles or all) so I can find convenient options
- I want to see practitioner contact info clearly so I can reach out directly
- I want to see what services they offer so I know if they meet my needs

#### As a Practitioner
- I want patients to find me easily so I can grow my business
- I want control over what info I share so I can protect my privacy
- I want a simple way to join the directory so I don't waste time on tech

#### As an Admin
- I want to verify practitioner licenses easily so only qualified providers are listed
- I want to add/remove practitioners manually so I control quality
- I want to see basic usage stats so I can report to stakeholders

---

## Functional Requirements

### Search & Discovery
- **FR-1:** System SHALL accept 3 location input types:
  - User's current geolocation (browser API)
  - 5-digit zip code
  - Full street address
- **FR-2:** System SHALL calculate distance from search location to each practitioner
- **FR-3:** System SHALL filter results by distance: 5mi, 10mi, 20mi, or "all"
- **FR-4:** System SHALL display results in order of proximity (nearest first)
- **FR-5:** System SHALL paginate results (display 10-20 per page)

### Practitioner Display
- **FR-6:** Each result SHALL display:
  - Full name
  - Services offered (list/tags)
  - Contact method (phone and/or email)
  - Distance from search location
  - (Optional) General area (e.g., "Portland Metro", not exact address)
- **FR-7:** Contact info SHALL be displayed directly (no request button/form in MVP)

### Data Management (Admin)
- **FR-8:** Admin SHALL manually update practitioner data via CSV/JSON file upload
- **FR-9:** System SHALL validate practitioner data format before displaying
- **FR-10:** System SHALL log search events (what/when/where) for future analytics
- **FR-11:** System SHALL support future role-based access (structure only, not implemented in MVP)

---

## Non-Functional Requirements

### Performance
- **NFR-1:** Search results SHALL load within 2 seconds under normal conditions
- **NFR-2:** Map display SHALL render within 3 seconds
- **NFR-3:** System SHALL support 100 concurrent users (far exceeds expected load)

### Scalability
- **NFR-4:** Database design SHALL support 10,000+ practitioners (future expansion)
- **NFR-5:** Architecture SHALL support adding admin panel without major refactor

### Security & Privacy
- **NFR-6:** Practitioner contact info SHALL only be visible to end users (not crawlable/scrapable)
- **NFR-7:** System SHALL use HTTPS for all connections
- **NFR-8:** Admin functions SHALL require authentication (even if manual in MVP)

### Usability
- **NFR-9:** UI SHALL be mobile-responsive (works on phones/tablets)
- **NFR-10:** Search SHALL work without account creation (low friction)
- **NFR-11:** Contact info SHALL be tap-to-call/email on mobile devices

### Maintainability
- **NFR-12:** Code SHALL follow .NET best practices (SOLID principles, clean architecture)
- **NFR-13:** Database migrations SHALL be scripted and version-controlled
- **NFR-14:** README SHALL document setup, deployment, and common tasks

---

## Technical Architecture

### High-Level Architecture Diagram
```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│   Patient   │         │ Practitioner │         │    Admin    │
│  (Mobile/   │         │  (Future)    │         │ (Manual)    │
│   Web)      │         │              │         │             │
└──────┬──────┘         └──────────────┘         └──────┬──────┘
       │                                                  │
       │  HTTPS                                          │  Upload CSV
       │                                                  │
       ▼                                                  ▼
┌─────────────────────────────────────────────────────────────┐
│           .NET MAUI Frontend (Blazor Hybrid)                │
│  ┌────────────┐  ┌────────────┐  ┌──────────────┐         │
│  │  Search    │  │  Results   │  │  Map View    │         │
│  │  Component │  │  List      │  │  Component   │         │
│  └────────────┘  └────────────┘  └──────────────┘         │
└────────────────────────┬────────────────────────────────────┘
                         │  REST API Calls (JSON)
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              ASP.NET Core Web API                           │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │ Search       │  │ Practitioner │  │ Admin        │     │
│  │ Controller   │  │ Controller   │  │ Controller   │     │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘     │
│         │                  │                  │             │
│  ┌──────▼──────────────────▼──────────────────▼────────┐   │
│  │          Business Logic Layer                        │   │
│  │  - Distance calculations (Haversine or PostGIS)     │   │
│  │  - Search filtering & sorting                        │   │
│  │  - Data validation                                   │   │
│  └──────────────────────┬───────────────────────────────┘   │
│                         │                                    │
│  ┌──────────────────────▼───────────────────────────────┐   │
│  │    Entity Framework Core (ORM)                       │   │
│  │  - Practitioner entity                               │   │
│  │  - SearchLog entity (for analytics)                  │   │
│  │  - (Future) User, Role entities                      │   │
│  └──────────────────────┬───────────────────────────────┘   │
└─────────────────────────┼───────────────────────────────────┘
                          │  SQL Queries
                          ▼
┌─────────────────────────────────────────────────────────────┐
│                 PostgreSQL Database                         │
│  ┌────────────────────────────────────────────────────┐    │
│  │  Tables:                                            │    │
│  │  - practitioners (id, name, lat, lon, services...) │    │
│  │  - search_logs (timestamp, location, filters...)   │    │
│  │  - (future) users, roles, sessions                 │    │
│  └────────────────────────────────────────────────────┘    │
│  ┌────────────────────────────────────────────────────┐    │
│  │  PostGIS Extension (geospatial queries)            │    │
│  └────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────┘

External Services:
┌─────────────┐
│  Mapbox or  │ ← Geocoding (address → lat/lon)
│ Google Maps │ ← Map display
└─────────────┘
```

---

## Data Model

### Practitioners Table (Core Entity)
```sql
CREATE TABLE practitioners (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    
    -- Identity
    full_name VARCHAR(255) NOT NULL,
    license_number VARCHAR(50) NOT NULL UNIQUE,
    license_expiration DATE NOT NULL,
    
    -- Contact (practitioner controls visibility)
    phone VARCHAR(20),
    email VARCHAR(255),
    
    -- Location (for distance calculations)
    latitude DECIMAL(10, 8) NOT NULL,
    longitude DECIMAL(11, 8) NOT NULL,
    display_location VARCHAR(255), -- e.g., "Portland Metro" (optional, practitioner-controlled)
    zip_code VARCHAR(5),
    
    -- Services
    services TEXT[], -- Array: ["cleanings", "x-rays", "fillings", "anesthesia"]
    
    -- Admin metadata
    is_active BOOLEAN DEFAULT true,
    verified_by VARCHAR(255), -- Admin who verified
    verified_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    -- Geospatial index for PostGIS
    location GEOGRAPHY(POINT, 4326) -- Generated from lat/lon
);

CREATE INDEX idx_practitioners_location ON practitioners USING GIST(location);
CREATE INDEX idx_practitioners_is_active ON practitioners(is_active);
```

### Search Logs Table (For Future Analytics)
```sql
CREATE TABLE search_logs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    
    -- Search parameters
    search_latitude DECIMAL(10, 8),
    search_longitude DECIMAL(11, 8),
    search_zip_code VARCHAR(5),
    distance_filter INTEGER, -- 5, 10, 20, or NULL (all)
    
    -- Results
    results_count INTEGER,
    
    -- Metadata
    searched_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    user_agent TEXT, -- Device/browser info
    ip_address INET -- For geographic distribution analysis (anonymize after 30 days)
);

CREATE INDEX idx_search_logs_searched_at ON search_logs(searched_at);
```

### Future Tables (Not Implemented in MVP, but structure supported)
```sql
-- Users table (for practitioner/admin login)
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL, -- 'admin', 'practitioner', 'patient'
    practitioner_id UUID REFERENCES practitioners(id), -- If role = practitioner
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Contact requests table (if we add "request contact" feature)
CREATE TABLE contact_requests (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    practitioner_id UUID REFERENCES practitioners(id),
    patient_name VARCHAR(255),
    patient_phone VARCHAR(20),
    patient_email VARCHAR(255),
    message TEXT,
    status VARCHAR(50), -- 'pending', 'contacted', 'expired'
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

---

## API Endpoints (MVP)

### Public Endpoints (No Auth Required)

#### `GET /api/search`
Search for practitioners by location and distance.

**Query Parameters:**
- `lat` (decimal, optional): Latitude of search center
- `lon` (decimal, optional): Longitude of search center
- `zip` (string, optional): 5-digit zip code (alternative to lat/lon)
- `address` (string, optional): Full street address (alternative to lat/lon)
- `distance` (integer, optional): Filter radius in miles (5, 10, 20, or omit for all)
- `page` (integer, optional): Page number for pagination (default: 1)
- `pageSize` (integer, optional): Results per page (default: 20, max: 50)

**Response:**
```json
{
  "searchLocation": {
    "lat": 45.5152,
    "lon": -122.6784
  },
  "totalResults": 37,
  "page": 1,
  "pageSize": 20,
  "practitioners": [
    {
      "id": "uuid-here",
      "name": "Jane Smith, EDHS",
      "services": ["cleanings", "x-rays", "fillings"],
      "phone": "503-555-0123",
      "email": "jane@example.com",
      "distanceMiles": 2.4,
      "displayLocation": "Portland Metro"
    },
    // ... more results
  ]
}
```

#### `GET /api/practitioners/{id}`
Get details for a specific practitioner.

**Response:**
```json
{
  "id": "uuid-here",
  "name": "Jane Smith, EDHS",
  "services": ["cleanings", "x-rays", "fillings", "anesthesia"],
  "phone": "503-555-0123",
  "email": "jane@example.com",
  "displayLocation": "Portland Metro",
  "licenseExpiration": "2026-12-31"
}
```

---

### Admin Endpoints (Auth Required - Future)

#### `POST /api/admin/practitioners`
Add a new practitioner (manual admin action in MVP, API designed for future admin panel).

**Request Body:**
```json
{
  "fullName": "Jane Smith",
  "licenseNumber": "EDHS-12345",
  "licenseExpiration": "2026-12-31",
  "phone": "503-555-0123",
  "email": "jane@example.com",
  "latitude": 45.5152,
  "longitude": -122.6784,
  "displayLocation": "Portland Metro",
  "zipCode": "97201",
  "services": ["cleanings", "x-rays", "fillings"]
}
```

#### `PUT /api/admin/practitioners/{id}`
Update practitioner info.

#### `DELETE /api/admin/practitioners/{id}`
Deactivate a practitioner (soft delete - sets `is_active = false`).

#### `GET /api/admin/stats`
Get basic usage statistics (for future dashboard).

**Response:**
```json
{
  "totalPractitioners": 37,
  "activePractitioners": 35,
  "totalSearches": 1243,
  "searchesThisWeek": 87,
  "topSearchLocations": [
    {"zip": "97201", "count": 156},
    {"zip": "97301", "count": 89}
  ]
}
```

---

## Technology Stack Details

### Backend (.NET)
- **Framework:** ASP.NET Core 8.0 (LTS)
- **ORM:** Entity Framework Core 8.0
- **Database Driver:** Npgsql (PostgreSQL)
- **Geospatial:** NetTopologySuite (for PostGIS integration)
- **API Documentation:** Swagger/OpenAPI (built-in)

**NuGet Packages:**
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.*" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.*" />
<PackageReference Include="NetTopologySuite" Version="2.5.*" />
<PackageReference Include="Npgsql.NetTopologySuite" Version="8.0.*" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.*" />
```

---

### Frontend (.NET MAUI)
- **Framework:** .NET MAUI 8.0 with Blazor Hybrid
- **UI Library:** MudBlazor (Material Design components for Blazor)
- **Maps:** Mapbox SDK or Google Maps SDK
- **HTTP Client:** Built-in HttpClient (calls Web API)

**NuGet Packages:**
```xml
<PackageReference Include="Microsoft.Maui.Controls" Version="8.0.*" />
<PackageReference Include="Microsoft.AspNetCore.Components.WebView.Maui" Version="8.0.*" />
<PackageReference Include="MudBlazor" Version="6.11.*" />
```

---

### Infrastructure
- **Hosting:** Azure App Service (Linux, B1 tier = $13/month)
- **Database:** Railway PostgreSQL (free tier: 512MB, upgrades to $5/month for 1GB)
- **CI/CD:** GitHub Actions (free for public repos)
- **Domain:** Namecheap or Cloudflare ($12/year for .com)
- **SSL:** Let's Encrypt (free, auto-renewal via Azure/Railway)

---

## Project Structure ("Leave Room For" Scaffolding)

```
ORToothFairy/
├── README.md                          # High-level project overview
├── .gitignore                         # Standard .NET gitignore
├── ORToothFairy.sln                     # Solution file
│
├── src/
│   ├── ORToothFairy.API/                # ASP.NET Core Web API
│   │   ├── Controllers/
│   │   │   ├── SearchController.cs
│   │   │   ├── PractitionersController.cs
│   │   │   └── AdminController.cs     # Stubbed for future
│   │   ├── Models/                    # DTOs, view models
│   │   ├── Services/                  # Business logic
│   │   │   ├── SearchService.cs
│   │   │   └── (future) NotificationService.cs
│   │   ├── Data/
│   │   │   ├── EDHSDbContext.cs       # EF Core context
│   │   │   └── Migrations/
│   │   ├── appsettings.json
│   │   └── Program.cs
│   │
│   ├── ORToothFairy.Core/               # Shared domain logic
│   │   ├── Entities/
│   │   │   ├── Practitioner.cs
│   │   │   ├── SearchLog.cs
│   │   │   └── (future) User.cs       # Commented out for MVP
│   │   ├── Interfaces/                # Repository patterns
│   │   └── Constants/
│   │
│   └── ORToothFairy.MAUI/               # .NET MAUI + Blazor app
│       ├── Pages/
│       │   ├── Search.razor           # Search page
│       │   ├── Results.razor          # Results list
│       │   └── PractitionerDetail.razor
│       ├── Components/
│       │   ├── MapComponent.razor
│       │   └── DistanceFilter.razor
│       ├── Services/
│       │   └── ApiService.cs          # HTTP client for API calls
│       ├── MauiProgram.cs
│       └── wwwroot/                   # CSS, images
│
├── tests/
│   ├── ORToothFairy.API.Tests/          # API unit tests
│   └── ORToothFairy.Core.Tests/         # Domain logic tests
│
├── docs/
│   ├── 01_Discovery.md                # This file
│   ├── 02_Requirements_And_Planning.md
│   ├── 03_Legal_Business_Setup.md
│   ├── 04_Build_MVP.md                # Future: dev journal, decisions
│   ├── 05_Launch_And_Iterate.md       # Future: post-launch learnings
│   │
│   ├── architecture/
│   │   ├── system-design.md           # Diagrams, decisions
│   │   └── database-schema.md         # Detailed schema docs
│   │
│   ├── api/
│   │   └── endpoints.md               # API documentation (supplement to Swagger)
│   │
│   └── future-features/               # "Leave room for" documentation
│       ├── README.md                  # Overview of planned features
│       ├── admin-panel.md             # Admin UI specs (not built yet)
│       ├── notifications.md           # Push notification system (not built)
│       └── analytics-dashboard.md     # Stats/reporting UI (not built)
│
└── scripts/
    ├── seed-data.sql                  # Sample practitioners for testing
    └── deploy.sh                      # Deployment automation (future)
```

### "Leave Room For" Implementation

**Key Strategy:** Create folders and stub files with clear comments explaining future intent.

**Example - `future-features/README.md`:**
```markdown
# Future Features (Not in MVP)

This directory documents features that are architecturally supported but not yet implemented.

## Planned Features
- **Admin Panel:** Web UI for managing practitioners (API endpoints exist, UI doesn't)
- **Notifications:** Push notifications when patients request contact (data model supports it)
- **Analytics Dashboard:** Usage stats visualization (data is logged, not displayed)

## Why Document Unbuilt Features?
- Shows planning & architectural thinking to employers
- Prevents painting ourselves into corners
- Makes it easy to add features later without refactors

## When to Build These
- Admin Panel: After 10+ practitioners request self-service
- Notifications: After practitioners request it
- Analytics: After we have 100+ searches/week
```

**Example - Commented code in `AdminController.cs`:**
```csharp
// TODO: Admin Panel Endpoints (Not in MVP)
// These routes are stubbed to show future architecture.
// Uncomment and implement when ready for self-service admin UI.

// [HttpGet("dashboard")]
// public async Task<IActionResult> GetDashboard()
// {
//     // Return stats for admin dashboard
//     // See: docs/future-features/analytics-dashboard.md
// }
```

---

## Development Milestones

### Milestone 1: Project Setup (Week 1, ~8 hours)
- [x] Discovery & Requirements docs complete
- [ ] Solution structure created
- [ ] Database schema designed and migrated
- [ ] Basic API skeleton (no logic, just structure)
- [ ] MAUI project initialized
- [ ] Hello World deployed to Azure (empty API responding)

**Deliverable:** Empty project that compiles and deploys.

---

### Milestone 2: Core Search Logic (Week 2-3, ~20 hours)
- [ ] Practitioner entity & EF Core context
- [ ] Seed 10 sample practitioners (CSV → database)
- [ ] Search API endpoint (accept lat/lon, return all practitioners)
- [ ] Distance calculation logic (Haversine formula)
- [ ] Distance filtering (5/10/20 mi)
- [ ] Unit tests for search logic

**Deliverable:** API endpoint that returns practitioners sorted by distance.

---

### Milestone 3: Frontend Search UI (Week 3-4, ~20 hours)
- [ ] Search page with 3 input types (geolocation, zip, address)
- [ ] Call Search API from MAUI app
- [ ] Display results in list view
- [ ] Distance filter dropdown
- [ ] Tap phone/email to call/compose

**Deliverable:** Working mobile app that can find practitioners.

---

### Milestone 4: Maps Integration (Week 5, ~15 hours)
- [ ] Mapbox/Google Maps SDK integrated
- [ ] Display practitioners on map (pins)
- [ ] Click pin → show practitioner details
- [ ] Geocoding (address → lat/lon)

**Deliverable:** Map view showing practitioner locations.

---

### Milestone 5: Admin Tools (Week 6, ~10 hours)
- [ ] CSV upload script (command-line or simple web form)
- [ ] License expiration checker (SQL query or simple tool)
- [ ] Search logging implementation
- [ ] Basic stats endpoint (even if not displayed)

**Deliverable:** Admin can add practitioners via CSV.

---

### Milestone 6: Polish & Deploy (Week 7-8, ~15 hours)
- [ ] Error handling & loading states
- [ ] Responsive design refinements
- [ ] Performance testing (100 practitioners)
- [ ] Documentation (README, API docs)
- [ ] Deploy to production (Azure + app stores if ready)

**Deliverable:** Production-ready v1.0.

---

### Post-MVP (Future Iterations)
- [ ] Admin panel UI
- [ ] Practitioner self-service signup
- [ ] Analytics dashboard
- [ ] Notification system
- [ ] Alaska expansion
- [ ] Other trade union apps

---

## Development Environment Setup

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code + C# extension
- PostgreSQL (local or Docker)
- Git
- Xcode (for iOS development, Mac only)
- Android Studio (for Android development)

### Local Development Setup
```bash
# Clone repo
git clone https://github.com/yourusername/ORToothFairy.git
cd ORToothFairy

# Restore packages
dotnet restore

# Set up database (update connection string in appsettings.json)
dotnet ef database update --project src/ORToothFairy.API

# Run API locally
cd src/ORToothFairy.API
dotnet run
# API runs on https://localhost:5001

# Run MAUI app (separate terminal)
cd src/ORToothFairy.MAUI
dotnet build -t:Run -f net8.0-android
# Or -f net8.0-ios, or -f net8.0-windows
```

### Environment Variables
```bash
# appsettings.Development.json (local only, not committed)
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ortooth-dev;Username=postgres;Password=yourpassword"
  },
  "MapboxApiKey": "your-mapbox-key-here",
  "GoogleMapsApiKey": "your-google-key-here"
}
```

---

## Testing Strategy

### Unit Tests
- Core business logic (distance calculations, filtering)
- Entity validation
- API endpoint responses

### Integration Tests
- Database queries (EF Core → PostgreSQL)
- API endpoints end-to-end
- Geocoding API integration

### Manual Testing
- Search from different locations
- Filter by distance
- Tap-to-call/email on mobile
- Admin CSV upload

### Performance Testing
- Load 400 practitioners, measure search speed
- Test with 100 concurrent requests (locust.io)

---

## Deployment Strategy

### Phase 1: Web URL (Week 8)
- Deploy API to Azure App Service
- Deploy database to Railway
- Share URL with neighbor (mobile-responsive web)
- Validate with 5-10 test practitioners

### Phase 2: App Stores (Week 10-12)
- Test on physical iOS/Android devices
- Submit to Apple App Store (review takes 1-3 days)
- Submit to Google Play Store (review takes few hours)
- Announce to practitioners

### Continuous Deployment
- GitHub Actions workflow:
  1. Run tests on push to `main`
  2. Build Docker image
  3. Deploy to Azure staging slot
  4. Smoke test staging
  5. Swap to production

---

## Risks & Mitigations (Technical)

### Risk: MAUI is new, limited resources
**Mitigation:** If blocked >1 day, fall back to Blazor Server (web-only, still modern)

### Risk: Geocoding API costs exceed budget
**Mitigation:** Cache geocoded addresses (zip → lat/lon), use free tier (should be enough)

### Risk: PostgreSQL free tier runs out
**Mitigation:** 400 practitioners = <1MB data. Free tier is 512MB. We're fine.

### Risk: App store rejection
**Mitigation:** Follow guidelines, no IAP/subscriptions in v1 (just contact display)

### Risk: Performance issues with PostGIS
**Mitigation:** Start with Haversine formula (pure C#), add PostGIS only if needed

---

## Success Metrics (Technical)

### Must Have (MVP)
- Search returns results <2 sec
- App runs on iOS 15+, Android 8+
- Zero crashes in 100 test searches
- 90%+ uptime

### Nice to Have
- <1 sec search results
- Works offline (cached results)
- Accessibility (screen reader support)

---

## Open Questions

- [ ] Do we need offline mode? (Low priority, requires local caching)
- [ ] Should we show practitioner photos? (HIPAA-adjacent, privacy concerns)
- [ ] Analytics: How long to keep search logs? (GDPR-style: 30 days?)
- [ ] How do practitioners update their own info in v1? (Email admin → manual update)

---

## Next Steps
1. Review this doc with neighbor (validate requirements)
2. Set up GitHub repo with structure above
3. Create Milestone 1 issues in GitHub Projects
4. Move to Legal & Business Setup phase (LLC, agreements, liability)
5. Start coding! 🎉

---

**Status:** Ready to proceed to Phase 3 (Legal & Business Setup).
