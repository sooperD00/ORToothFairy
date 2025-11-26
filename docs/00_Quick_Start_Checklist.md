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

### Milestone 4: UI/UX ✅ COMPLETE
**Goal:** A UI that inspires adoption by Practitioners and Clients

#### Home Page
- [x] Two stock photo cards: "Individuals & Families" / "Business & Facilities"
- [x] Click navigates directly to purpose-built search page (no accordions)
- [x] Clean, minimal friction entry point

#### Individuals & Families Search (`/individuals-and-families`)
- [x] Hero section with family photo + gradient overlay
- [x] Frosted-glass triage card explaining use cases (house calls, care facilities, dental offices, community centers)
- [x] Search by location, zip, or address
- [x] Distance filter
- [x] Services filter placeholder with "Coming soon" badge
- [x] Business-card style results with services chips
- [x] Contact buttons (Call/Text/Email) shown only if accepted
- [x] Favorites (❤️) with local storage persistence

#### Business & Facilities Search (`/business-and-facilities`)
- [x] Hero section with facility photo + gradient overlay
- [x] Staffing type selector (Full-time / Part-time / Coverage / Emergency)
- [x] Same search functionality as B2C
- [x] "Bench" feature (⭐) separate from B2C favorites
- [x] Business interest capture form (name, email, org type)
- [x] "Coming soon" messaging for dashboard features

#### Practitioner Cards
- [x] Name + distance badge
- [x] City/zip location
- [x] Services as chips (max 4 + "more" indicator)
- [x] Call/Text/Email action buttons (conditional on AcceptsCalls/AcceptsTexts)
- [x] Directions button (opens Maps)
- [x] Favorite/Bench toggle

#### About Page
- [x] Clean copy explaining the app
- [x] "How It Works" steps
- [x] FAQ with B2C, B2B, and hygienist questions
- [x] Contact link

#### Header & Navigation
- [x] "ORToothFairy" branding
- [x] About link
- [x] Hamburger menu (How it Works, Contact, For Hygienists, etc.)

#### Cleanup
- [x] Removed ProfilePage/ClientProfile entities (over-engineered for MVP)
- [x] Removed old Search.razor
- [x] Consolidated CSS with `if-` and `bf-` prefixes

---

### Post-MVP / Future
- [ ] Services filter (need hygienist feedback on categories)
- [ ] Legal page (Terms of Service, Privacy Policy)
- [ ] API endpoint to capture business interest form
- [ ] API endpoint to track favorite/bench counts (analytics)
- [ ] Responsive design polish (test on actual devices)
- [ ] "For Hygienists" registration flow
- [ ] Remember user's last path (B2C vs B2B) for return visits

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
