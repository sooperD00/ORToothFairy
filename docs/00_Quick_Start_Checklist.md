# EDHS Finder - Quick Start Checklist

**Your roadmap from idea → deployed app**

---

## Quick Links

- **Milestones:** https://github.com/sooperD00/ORToothFairy/milestones
- **Issues:** https://github.com/sooperD00/ORToothFairy/issues
- **Actions (CI/CD):** https://github.com/sooperD00/ORToothFairy/actions

---

## ✅ Phase 1: Discovery (COMPLETE)
- [x] Validated problem with domain expert (neighbor)
- [x] Identified user profiles (patients, practitioners, admins)
- [x] Defined MVP scope (search + display, no admin panel yet)
- [x] Chose tech stack (.NET MAUI + ASP.NET Core)
- [x] Documented in `01_Discovery.md`

**Key Decision:** Build native apps from start (MAUI) for better portfolio + neighbor's preference.

---

## ✅ Phase 2: Requirements & Planning (COMPLETE)
- [x] Defined functional requirements (search, filter, display)
- [x] Designed database schema (practitioners, search_logs)
- [x] Mapped API endpoints
- [x] Created project structure with "leave room for" scaffolding
- [x] Set milestones (8 weeks, 80-120 hours)
- [x] Documented in `02_Requirements_And_Planning.md`

**Key Decision:** PostgreSQL + PostGIS for geospatial queries, EF Core for ORM.

---

## 🔄 Phase 3: Legal & Business (Neighbor and Husband are mulling - suggestions seem good - revisit in Early December!)

### Immediate Actions
- [ ] **Talk to lawyer husband** (1 hour)
  - Conflict of interest for neighbor?
  - LLC or contractor model?
  - Liability concerns?
  
- [x] **Discuss with neighbor** (30 min)
  - Ownership split (50/50 or other?)
  - Revenue split
  - Time commitments
  
- [x] **Decide on business structure**
  - Option A: No entity (hobby project)
  - Option B: Join her LLC
  - Option C: New LLC together ← we agree this is best
  - Option D: You as contractor

### If Forming LLC - Cris will own, start in Dec
- [ ] File Oregon LLC paperwork (~$100, online, 1 week)
- [ ] Get EIN from IRS (free, 15 min online)
- [ ] Open business bank account
- [ ] Draft operating agreement (lawyer can help)

### Legal Docs (Regardless of Structure)
- [ ] Write Terms of Service (use template, customize)
- [ ] Write Privacy Policy (use template, customize)
- [ ] Get lawyer to review (optional but smart)

**Timeline:** 1-3 weeks depending on LLC decision

**See:** `03_Legal_Business_Setup.md` for details

---

## 🚧 Phase 4: Build MVP (IN PARALLEL)

### Milestone 1: Project Setup (Week 1, ~8 hours)
- [x] Create GitHub repo (building in public with MIT License because portfolio)
- [x] Set up solution structure (see `02_Requirements_And_Planning.md`)
- [x] Initialize .NET projects:
  ```bash
  dotnet new webapi -n EDHSFinder.API
  dotnet new maui-blazor -n EDHSFinder.MAUI
  dotnet new classlib -n EDHSFinder.Core
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
- [ ] Write search logic (distance calculation)
- [ ] Write unit tests (search service - TDD style)
- [ ] Create Search API endpoint
- [ ] **Deliverable:** API returns practitioners sorted by distance

### Milestone 3: Frontend Search UI (Week 3-4, ~20 hours)
- [ ] Build Search page (MAUI Blazor)
- [ ] Add 3 input types (geolocation, zip, address)
- [ ] Call Search API from app
- [ ] Display results in list
- [ ] Add distance filter dropdown
- [ ] Make phone/email tappable
- [ ] **Deliverable:** Working app that finds practitioners

### Milestone 4: Maps Integration (Week 5, ~15 hours)
- [ ] Integrate Mapbox or Google Maps SDK
- [ ] Show practitioners on map (pins)
- [ ] Click pin → show details
- [ ] Add geocoding (address → lat/lon)
- [ ] **Deliverable:** Map view works

### Milestone 5: Admin Tools (Week 6, ~10 hours)
- [ ] Create CSV upload script (command-line is fine)
- [ ] Add search logging to database
- [ ] Create basic stats query (even if not displayed)
- [ ] **Deliverable:** Admin can add practitioners via CSV

### Milestone 6: Polish & Deploy (Week 7-8, ~15 hours)
- [ ] Error handling & loading states
- [ ] Responsive design polish
- [ ] Performance testing
- [ ] Write README & API docs
- [ ] Deploy to production (Azure)
- [ ] Submit to app stores (if ready)
- [ ] **Deliverable:** Production v1.0 live!

---

## 📱 Phase 5: Launch & Iterate (FUTURE)

- [ ] Get 5-10 test practitioners to try it
- [ ] Collect feedback
- [ ] Fix bugs
- [ ] Market to union members (neighbor's job)
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
cd src/EDHSFinder.API
dotnet run
# Runs on https://localhost:5001

# Run MAUI app
cd src/EDHSFinder.MAUI
dotnet build -t:Run -f net8.0-android  # Android
dotnet build -t:Run -f net8.0-ios      # iOS (Mac only)
```

### Database Migrations
```bash
# Create new migration
dotnet ef migrations add MigrationName --project src/EDHSFinder.API

# Apply migrations
dotnet ef database update --project src/EDHSFinder.API

# Seed test data
psql -U postgres -d edhsfinder -f scripts/seed-data.sql
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
az webapp up --name edhsfinder-api --resource-group edhsfinder-rg
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
1. **Claude** (that's me! I can debug code, explain concepts)
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
- ✅ Portfolio piece that gets you interviews

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

**Step 1:** Talk to lawyer husband (1 hour this week)  
**Step 2:** Talk to neighbor about ownership split (30 min)  
**Step 3:** Decide on LLC or not (by end of week)  
**Step 4:** Set up GitHub repo (30 min)  
**Step 5:** Start Milestone 1 (project setup)

---

**Remember:** Deploy early, deploy often. Get something in her hands ASAP, even if imperfect. You can always iterate!

🚀 **You got this!**
