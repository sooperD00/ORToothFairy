# OR Tooth Fairy

**Connecting Oregonians with dental hygienists — in homes, care facilities, and communities.**

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-Latest-blue)](https://dotnet.microsoft.com/apps/maui)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

## Project Status: MVP Complete — Collecting Feedback

The app is feature-complete for initial demos. Currently gathering feedback from Oregon dental hygienists to inform data structures and admin tooling.

| What's Done | What's Next |
|-------------|-------------|
| ✅ Location-based search (GPS, zip, address) | Deploy to production (Azure) |
| ✅ Distance filtering | Hygienist demos & feedback |
| ✅ B2C flow (individuals & families) | Iterate on services/data model |
| ✅ B2B flow (businesses & facilities) | App store submission |
| ✅ Contact flows (general + hygienist signup) | Admin tools (post-feedback) |

---

## What is ORToothFairy?

Oregon dental hygienists can practice in settings beyond traditional dental offices — homes, nursing facilities, schools, community health centers. But there's no way to find them. ORToothFairy fixes that.

**For individuals & families:** Find hygienists who make house calls or work in your community.

**For businesses & facilities:** Find hygienists for staffing — full-time, part-time, coverage, or emergency fill-ins.

**For hygienists:** Get discovered by patients and organizations who need you.

This is a real product built in partnership with the Oregon Dental Hygienists' Association, with planned monetization ($15/month practitioner listings, B2B subscription tiers).

---

## Skills Demonstrated

This isn't a tutorial project — it's a functioning product with real users pending. That said, it showcases:

| Area | What's Here |
|------|-------------|
| **Architecture** | Greenfield design: requirements → schema → API → UI |
| **Geospatial** | Location search with distance calculations, geocoding |
| **Cross-platform** | .NET MAUI Blazor Hybrid (Windows, iOS, Android from one codebase) |
| **Product thinking** | B2C and B2B flows, user research, MVP scoping |
| **Shipped** | Not a WIP — working app ready for user feedback |

Source: 13+ years building distributed systems and real-time analytics at Intel.

---

## Features

### For Individuals & Families
- 🔍 Search by GPS, zip code, or address
- 📏 Distance filtering (5/10/25/50 miles)
- 📱 Tap to call, text, or email practitioners
- ❤️ Save favorites locally

### For Businesses & Facilities
- 🏥 Same search, tailored messaging
- 📋 Staffing type selector (full-time, part-time, coverage, emergency)
- ⭐ Build a "bench" of preferred hygienists
- 📝 Interest capture for premium features

### For Hygienists
- 📍 Get listed and discovered
- 📞 Direct patient contact (no middleman)
- 🏷️ Service and location visibility

### Planned: Admin & Analytics
- Search logging and usage stats
- Registration approval workflow
- Favorite/bench aggregate analytics
- Services filter (pending hygienist feedback on categories)

---

## Screenshots

| Home | Search Results | B2B Flow |
|------|----------------|----------|
| ![Home](docs/screenshots/home.png) | ![Results](docs/screenshots/results.png) | ![B2B](docs/screenshots/b2b.png) |

---

## 🛠️ Tech Stack & Architecture Decisions

| Component | Choice | Why |
|-----------|--------|-----|
| **Backend** | ASP.NET Core 9.0 Web API | Modern, async, production-ready |
| **Frontend** | .NET MAUI (Blazor Hybrid) | Single codebase → Windows, iOS, Android |
| **Database** | SQLite (MVP) → PostgreSQL + PostGIS | Start simple, migrate when needed |
| **ORM** | Entity Framework Core | Type-safe, migrations, LINQ |
| **Geocoding** | Nominatim (OpenStreetMap) | Free, no API key for MVP |
| **Hosting** | Azure App Service (planned) | ~$30/mo at scale |

**Why this stack?**
- Portfolio-friendly (in-demand skills)
- Single C# codebase reduces complexity
- Scales from MVP to production
- PostGIS is purpose-built for location search

See `/docs/02_Requirements_And_Planning.md` → **Section: Tech Stack Selection** for detailed rationale (I evaluated 4+ alternatives).

---

## 🗺️ Roadmap

### ✅ Phase 1-2: Discovery & Planning (Complete)
Market validation, user research, tech decisions → [See docs/](docs/)

### ✅ Phase 3: Legal Setup (Complete)
LLC structure, Terms/Privacy Policy in progress

### ✅ Phase 4: MVP Development (Complete)
- **M1-4:** Core search, MAUI UI, geolocation, contact flows ✅
- **M5:** Admin Tools — DEFERRED (will design after hygienist feedback)
- **M6:** Polish & Deploy — IN PROGRESS

### 🔄 Phase 5: Launch & Iterate (Current)
- [ ] Deploy to production
- [ ] Demo to 5-10 hygienists
- [ ] Collect feedback on services/data model
- [ ] ODHA outreach (with Cris)

### 📮 Post-MVP
- Services filter (pending hygienist feedback)
- Admin tooling (registration approval flow)
- Analytics endpoints
- Multi-state expansion (if Oregon succeeds)

---

## 📁 Repository Structure
```
ORToothFairy/
├── docs/
│   ├── 01_Discovery.md
│   ├── 02_Requirements_And_Planning.md
│   └── 03_Legal_Business_Setup.md
├── src/
│   ├── ORToothFairy.API/          # ASP.NET Core backend
│   ├── ORToothFairy.Core/         # Shared domain logic
│   └── ORToothFairy.MAUI/         # Blazor Hybrid frontend
└── tests/
```

**👀 For hiring managers:** Check out my planning process in `/docs` folder.

---

## 🚀 Quick Start
```bash
# Clone
git clone https://github.com/sooperD00/ORToothFairy.git
cd ORToothFairy

# Run API
cd src/ORToothFairy.API
dotnet run  # Runs on https://localhost:5001

# Run MAUI app (separate terminal)
cd src/ORToothFairy.MAUI
dotnet run -f net9.0-windows10.0.19041.0
```

---

## 🤔 Why Build This in Public?

**Transparency over secrecy.** My competitive advantages are:
1. Partner's network (can't clone that)
2. Domain knowledge (I've done the research)
3. Actually shipping it (most people don't finish)

The code itself isn't proprietary—it's solid architecture applied to a real problem. **That's what employers want to see.**

If the business succeeds, great. If not, this is still a strong portfolio piece demonstrating senior-level thinking.

---

## Contact

**Nicole Rowsey** — Staff Data Platform Engineer | Distributed Systems | Real-Time Analytics | PhD EE

- 📧 nicole.rowsey@gmail.com
- 💼 [LinkedIn](https://linkedin.com/in/nicolerowsey)
- 💻 [GitHub](https://github.com/sooperD00)

Open to senior/staff IC roles in data engineering and platform work. Happy to discuss this project or others.

---

**Built with ❤️ and .NET** | [View Planning Docs](docs/) | [Future Features](docs/future-features/)