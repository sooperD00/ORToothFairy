# OR Tooth Fairy - Quick Start Checklist

**Roadmap: idea → deployed app (~8 Weeks!)**

---

## Quick Links

- **Milestones:** https://github.com/sooperD00/ORToothFairy/milestones
- **Issues:** https://github.com/sooperD00/ORToothFairy/issues
- **Actions (CI/CD):** https://github.com/sooperD00/ORToothFairy/actions

---

## ✅ Phase 1: Discovery (COMPLETE)
- [x] Validated problem with domain expert (Cris Bowerman - ED for Oregon Dental Hygenists Association)
- [x] Identified user profiles:
  - clients: direct patients, B2B (care facilities), dentist offices
  - practitioners: any dental hygienist in OR (EDHS and not EDHS as well)
  - admins: Nicole
- [x] Defined MVP scope (search + display, no admin panel yet)
- [x] Chose tech stack (.NET MAUI + ASP.NET Core)
- [x] Documented in `01_Discovery.md`

**Key Decision:** Build native apps from start (MAUI) for better portfolio + Cris's best guess at user preference.

---

## ✅ Phase 2: Requirements & Planning (COMPLETE)
- [x] Defined functional requirements (search, filter, display)
- [x] Designed database schema (practitioners, search_logs)
- [x] Mapped API endpoints
- [x] Created project structure with "leave room for" scaffolding
- [x] Set milestones (8 weeks, 100 hours)
- [x] Documented in `02_Requirements_And_Planning.md`

**Key Decision:** PostgreSQL + PostGIS for geospatial queries, EF Core for ORM.

---

## 🔄 Phase 3: Legal & Business (Nov-Dec)

### Roles and Responsibilites (Nov)
- **Cris**
  - Cris had the idea but cannot monetize - COI self dealing
  - She is happy as ED of Oregon Dental Hygienists Association
  - She is passionate about oral health care in Hillsboro and OR
  - Her financial portion can go to a foundation for local oral healthcare - Cris will set that up, Nicole will contribute 30-40% after costs covered

- **Nicole**
  - [x] Business structure = Nicole as sole LLC
  - [ ] ⚖️ Liability concerns?
  - [ ] Finish MVP (~Nov/Dec)
  - [ ] Get Practitioner feedback (Dec)
  - [ ] Get advice from local SBDC in (~Dec/Jan)

### Meet with SBDC (Dec)
 - [ ] Advice on LLC
 - [ ] Advice on Legal
 - [ ] Advice on Taxes
 - [ ] Advice on Parterns/board/paying advisors/???
 - [ ] Advice on Liability concerns
 - [ ] Get Lawyer and Accountant recs

### Form LLC (Dec/Jan)
- [ ] If positive feedback on MVP?
- [ ] File Oregon LLC paperwork (~$100, online, 1 week (~Dec/Jan))
- [ ] Get EIN from IRS (free, 15 min online)
- [ ] Open business bank account (Jan)
- [ ] ⚖️ Draft operating agreement (Jan)

### Legal Docs (Jan)
- [ ] ⚖️ Write Terms of Service (use template, customize)
- [ ] ⚖️ Write Privacy Policy (use template, customize)

### Bring to Lawyer (Jan)
- [ ] ⚖️ Liability concerns?
- [ ] ⚖️ Draft operating agreement (Jan)
- [ ] ⚖️ Write Terms of Service (use template, customize)
- [ ] ⚖️ Write Privacy Policy (use template, customize)

### Taxes (Feb?)


**See:** `03_Legal_Business_Setup.md` for details **todo: this needs update**

---

## 🚧 Phase 4: Build MVP (Nov-Dec)

### Milestone 1: Project Setup (Week 1, ~8 hours)
- [x] Create GitHub repo (building in public with MIT License because portfolio)
- [x] Set up solution structure (see `02_Requirements_And_Planning.md`)
- [x] Initialize .NET projects:
  ```bash
  dotnet new webapi -n ORToothFairy.API
  dotnet new maui-blazor -n ORToothFairy.MAUI
  dotnet new classlib -n ORToothFairy.Core
  ```
- [x] Add Entity Framework Core NuGet packages
- [x] Set up PostgreSQL (local Docker or Railway)
- [x] Create database schema (run first migration)
- [x] Deploy "Hello World" API to Azure
- [x] **Deliverable:** Empty project that compiles and deploys

### Milestone 2: Core Search Logic (Week 2-3, ~20 hours)
- [x] Create `Practitioner` entity class
- [x] Set up EF Core DbContext
- [x] Write unit tests (entity tests - basic coverage)
- [x] Seed 10 test practitioners (CSV → database)
- [x] Write search logic (distance calculation)
- [x] Write unit tests (search service - TDD style)
- [x] Create Search API endpoint
- [x] **Deliverable:** API returns practitioners sorted by distance

### Milestone 3: Frontend Search UI (Week 3-4, ~20 hours)
- [x] Build Search page (MAUI Blazor)
- [x] Add 3 input types (geolocation, zip, address)
- [x] Call Search API from app
- [x] Display results in list
- [x] Add distance filter dropdown
- [x] Make phone/email tappable
- [x] **Deliverable:** Working app that finds practitioners

### Milestone 4: UI/UX (Week 5, ~15 hours)
- [x] Integrate link to open address in Maps
- [ ] Create 4 (or however many) client profiles (probably in Core?)
- [ ] "Give an ask" to a new user to move them past friction
- [ ] (Optional): remember a client's last "profile" and select that on return
- [ ] Polish the Practitioner "card" in the UI (nice box, services offered, make it actually look like a nice business card (but not too much bc it's the web))
- [ ] Note V2.0 features while you remember them like 1) filter by practitioner services, 2) integrate number of miles practitioner willing to travel into search return logic, 3) feedback on list return fairness
- [ ] **Deliverable:** A UI that will inspire adoption by Practitioners and Clients


# Milestone 4: Client Profile UI Implementation

**Goal:** Build a flexible, profile-driven search UI that supports three page categories (Individuals/Families, B2B, Need Help) with configurable client profiles using EF Core entities.

## Architecture Changes

### New Entities
- [ ] Create `ClientProfile.cs` entity with properties:
  - [ ] Id (primary key)
  - [ ] HeadlineName (string) - brief profile name
  - [ ] ExpandedDescription (string) - education text shown on accordion expand
  - [ ] PageCategory (string) - "IndividualsAndFamilies", "B2B", or "NeedHelp"
  - [ ] DisplayOrder (int) - sort order within page
  - [ ] IsActive (bool) - show/hide for demos
  - [ ] DefaultSearchType (string) - "Geolocation", "Address", or "Zip"
  - [ ] HighlightColor (string) - hex color for gentle box around suggested search
  - [ ] ShowRadiusOption (bool)
  - [ ] DefaultRadiusMiles (int?)
  - [ ] UserStoryIds (string) - comma-delimited for documentation mapping

- [ ] Add `ClientProfile` DbSet to ApplicationDbContext
- [ ] Create and run migration for ClientProfile table

### Seed Data
- [ ] Create seed data for Individuals/Families page profiles
  - [ ] Profile 1: Basic individual search (geolocation default)
  - [ ] Profile 2: Care home resident visit variant (address default)
- [ ] Create seed data for B2B page profiles
  - [ ] Care facilities coordinator profile
  - [ ] Dentist office staffing profile
- [ ] Create seed data for Need Help page profiles
  - [ ] Community health center navigation profile
- [ ] Apply seed data in migration or DbContext OnModelCreating

## UI Components

### Page Structure
- [ ] Create three pages/routes:
  - [ ] `/search` or `/` - Individuals and Families
  - [ ] `/business` - B2B
  - [ ] `/help` - Need Help
- [ ] Each page filters ClientProfiles by PageCategory
- [ ] Each page orders profiles by DisplayOrder where IsActive = true

### Accordion Component
- [ ] Create ClientProfileAccordion component
  - [ ] Displays HeadlineName as collapsed header
  - [ ] Expands to show ExpandedDescription on click
  - [ ] Sets active profile state when clicked
  - [ ] Visual indicator for currently selected profile

### Search Component
- [ ] Reusable Search component (shared across all three pages)
- [ ] Three search input options always visible:
  - [ ] "Use my location" button (geolocation)
  - [ ] Zip code input field
  - [ ] Address input field
- [ ] Apply gentle highlight box (using profile's HighlightColor) around DefaultSearchType option
- [ ] Show radius selector if ShowRadiusOption = true, default to DefaultRadiusMiles
- [ ] On profile selection, update search defaults automatically

### Results Display
- [ ] Practitioner card component for results
- [ ] Order results by distance (closest first)
- [ ] Display 2-3 service tags/icons per practitioner (no filter yet)
- [ ] Cards show: name, distance, services, contact info

### Navigation
- [ ] Hamburger menu with links:
  - [ ] About page (includes FAQ)
  - [ ] Oregon Dental Hygienists Association (external)
  - [ ] OR Oral Health podcast (external)
  - [ ] Contact page
  - [ ] Legal page

## Testing & Polish
- [ ] Test profile swapping on each page
- [ ] Verify search defaults change based on selected profile
- [ ] Test distance ordering in results
- [ ] Responsive design check (mobile, tablet, desktop)
- [ ] Color contrast check for HighlightColor boxes
- [ ] Demo mode: toggle IsActive flags to show different profile combinations

## Nice-to-Haves (Post-MVP)
- Service filter UI
- UserStories entity table (if comma-delimited becomes unwieldy)
- SearchPreferencesJson for complex future preferences
- Profile analytics tracking

---

**Estimated Time:** 2-3 days
- Day 1: Entity setup, migrations, seed data, basic accordion (4-6 hrs)
- Day 2: Search component wiring, profile state management, results display (4-6 hrs)
- Day 3: Navigation, polish, testing profile combinations (3-4 hrs)

### Milestone 5: Admin Tools (Week 6, ~10 hours)
- [?] Create CSV upload script (command-line is fine) - **rethink this**
  - this needs to be re-considered in the design, because it will likely be me and not Cris, so table for now, but redesign soon, maybe not before MVP show-off and first feedbacks though, but as time allows while waiting for feedback
- [ ] Add search logging to database
- [ ] Create basic stats query (even if not displayed)
- [ ] **Deliverable:** Admin can add practitioners via ***undecided interface bc now it's for Nicole to manage, not Cris***

### Milestone 6: Polish & Deploy (Week 7-8, ~15 hours)
- [ ] Error handling & loading states
- [ ] Responsive design polish
- [ ] Add integration tests
- [ ] Check the Spinner in the UI while "waiting"
- [ ] Fix plural on MAUI razor "1 practitioner**s** found"
- [ ] make sure you have 6 decimals for the lat/lon for accuracy within 1cm
- [ ] show "Accepts Calls", "Accepts Texts", and "Services" list in UI results
- [ ] Performance testing
- [ ] Write README & API docs - this is a real project now, not a portfolio project. The "learnings" have to go - I want it 100% professional but also good and readable documentation for myself and all those --verbose engineers who are allowed to sit at my lunch table but who also can't sit through lunch (or read a novel of a document) because there's more fun stuff to do - it's a tight balance
- [ ] Deploy to production (Azure)
- [ ] Submit to app stores (if ready)
- [ ] **Deliverable:** Production v1.0 live!

---

## 📱 Phase 5: Launch & Iterate (FUTURE)

- [ ] Get 5-10 test practitioners to try it
- [ ] Collect feedback
- [ ] Fix bugs
- [ ] Market to "Oregon Dental Hygenists Association" members (with Cris)
- [ ] Monitor usage stats
- [ ] Plan v2 features based on data

---

## Quick Reference: Tech Stack

| Component | Technology | Why |
|-----------|------------|-----|
| Backend API | ASP.NET Core 8.0 | Jobs want it, you know it |
| Frontend/Mobile | .NET MAUI + Blazor | One codebase, web + iOS + Android |
| Database | PostgreSQL + PostGIS | Free tier, geo queries |
| ORM | Entity Framework Core | C# → SQL, clean & modern |
| Maps | Mapbox or Google Maps | Free tier sufficient |
| Hosting | Azure App Service | .NET-friendly, $15-30/mo |
| CI/CD | GitHub Actions | Free, integrates well |

---

## Common Commands (Save These)

### Local Development
```bash
# Run API locally
cd src/ORToothFairy.API
dotnet run
# Runs on https://localhost:5001

# Run MAUI app
cd src/ORToothFairy.MAUI
dotnet build -t:Run -f net8.0-android  # Android
dotnet build -t:Run -f net8.0-ios      # iOS (Mac only)
```

### Database Migrations
```bash
# Create new migration
dotnet ef migrations add MigrationName --project src/ORToothFairy.API

# Apply migrations
dotnet ef database update --project src/ORToothFairy.API

# Seed test data
psql -U postgres -d ortoothfairy -f scripts/seed-data.sql
```

### Testing
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### Deployment
```bash
# Build for production
dotnet publish -c Release

# Deploy to Azure (via GitHub Actions, or manual)
az webapp up --name ortoothfairy-api --resource-group ortoothfairy-rg
```

---

## Resources to Bookmark

### Documentation
- [.NET MAUI Docs](https://learn.microsoft.com/en-us/dotnet/maui/)
- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [PostGIS Functions](https://postgis.net/docs/reference.html)

### Tutorials
- [MAUI Blazor Hybrid Tutorial](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/)
- [Building APIs with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)

### Tools
- [Mapbox Docs](https://docs.mapbox.com/)
- [Railway Docs](https://docs.railway.app/)
- [Azure App Service](https://learn.microsoft.com/en-us/azure/app-service/)

---

## When You Get Stuck

### Common Issues & Solutions

**"MAUI build errors"**
- Clean solution: `dotnet clean && dotnet build`
- Check SDK version: `dotnet --version` (need 8.0+)
- Update workloads: `dotnet workload update`

**"Can't connect to database"**
- Check connection string in `appsettings.json`
- Verify PostgreSQL is running: `pg_isready`
- Check firewall/network (Azure → Railway connection)

**"Geolocation not working"**
- HTTPS required for browser geolocation
- Check app permissions (iOS/Android)
- Verify lat/lon are valid numbers

**"Distance calculations seem wrong"**
- Check Haversine formula units (radians vs degrees)
- Verify lat/lon order (lat is Y, lon is X)
- Use PostGIS `ST_Distance` if doing complex queries

### Where to Ask for Help
1. **Claude** (debug code, explain concepts)
2. **Stack Overflow** (search first, 90% of questions answered)
3. **.NET Discord/Reddit** (r/dotnet, r/csharp)
4. **MAUI GitHub Issues** (if you hit a framework bug)

---

## Success Metrics

### MVP Success (Portfolio Win)
- ✅ App deployed and accessible
- ✅ GitHub repo with clean structure
- ✅ Resume line: "Built cross-platform healthcare app with .NET MAUI"

### Business Success (Revenue)
- ✅ 10+ paying practitioners ($150/mo)
- ✅ Covers costs + modest side income
- ✅ Partnership validated for future projects

### Learning Success (Career Growth)
- ✅ Comfortable with .NET MAUI
- ✅ Can architect & deploy production apps
- ✅ Portfolio piece that gets interviews

---

## Emergency Pivots (If Things Go Wrong)

### If MAUI is too hard
→ Fall back to Blazor Server (web-only, simpler)
→ Still modern .NET, still portfolio-worthy

### If neighbor drops out
→ Finish as portfolio piece anyway
→ Pivot to different niche (e.g., other trade workers)

### If timeline stretches too long
→ Deploy v1 with just search (no maps, no polish)
→ "Shipped is better than perfect"

### If revenue doesn't materialize
→ Open-source the code (MIT license)
→ Use as case study for consulting work

---

## Your Next Action (Right Now)

**Step 1:** Deliver, baby, deliver
