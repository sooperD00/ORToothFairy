# OR Tooth Fairy

> ⚠️ **Work in Progress** — Building in public to demonstrate modern .NET architecture, product planning, and real-world problem solving.

**Connecting Oregon patients with independent dental hygienists (EDHS practitioners).**

**Why "Tooth Fairy"?** Many EDHS practitioners call themselves tooth fairies—they bring dental care to underserved communities like nursing homes, schools, and low-income populations. But patients can't find them. This app fixes that.

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-Latest-blue)](https://dotnet.microsoft.com/apps/maui)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

---

## 🎯 Why This Project Exists

### The Business Problem
Oregon has ~400 Expanded Practice Dental Hygienists (EDHS) who can provide dental care independently—but patients can't find them. Google doesn't work, no directory exists, and practitioners lose business. I'm partnering with a dental hygienist union ED to test if a simple finder app can solve this (and generate modest revenue at $15/practitioner/month).

### The Portfolio Goal
I'm transitioning from semiconductor manufacturing engineering to software development, targeting senior IC and data/engineering manager roles. **This project demonstrates:**

- **Greenfield architecture planning** (see `/docs/02_Requirements_And_Planning.md` for detailed technical specs)
- **Business-to-technical translation** (market research → database schema → API design)
- **Risk management** (MVP scope, "leave room for" future features, exit strategies)
- **Modern .NET skills** (.NET MAUI, ASP.NET Core, EF Core, PostGIS)
- **Real-world execution** (not a tutorial—actual constraints, trade-offs, deployment)

**📁 Start in `/docs/`** to see how I validated the problem and planned the solution before writing code. The requirements doc is particularly detailed.

**🚧 Current Status:** Legal/business setup phase (LLC structure, ToS/Privacy). Milestone 1 (project scaffolding) starts next.

---

## 🦷 What is EDHS?

Expanded Practice Dental Hygienists in Oregon can provide preventive dental care independently in underserved settings (nursing homes, schools, low-income populations). They're licensed, skilled, and needed—but invisible to the patients who need them most.

**This app is Yelp for independent dental hygienists.** Simple, focused, solves a real problem.

---

## ✨ MVP Features (8-week build)

### For Patients
- 🔍 Search by location (geolocation, zip, or address)
- 📏 Distance filtering (5/10/20 miles or show all)
- 📱 Tap to call/email practitioners directly
- 🗺️ Map view with pins
- 📱 Native mobile (iOS/Android) + web from single codebase

### For Practitioners
- 📝 Simple listing (contact info, services, license verification)
- 🎯 Patient discovery (currently impossible)
- 💰 Affordable ($15/month subscription)

### For Admins (Manual for MVP)
- ✅ License verification via CSV
- 📊 Usage logging (no dashboard yet—see `/docs/future-features/analytics-dashboard.md`)

---

## 🛠️ Tech Stack & Architecture Decisions

| Component | Choice | Why |
|-----------|--------|-----|
| **Backend** | ASP.NET Core 8.0 Web API | Modern, async, great for resume |
| **Frontend** | .NET MAUI (Blazor Hybrid) | Single codebase → web + iOS + Android |
| **Database** | PostgreSQL + PostGIS | Geospatial queries (distance calculations) |
| **ORM** | Entity Framework Core | Type-safe, migrations, LINQ |
| **Maps** | Mapbox / Google Maps | Standard integrations |
| **Hosting** | Azure App Service + Railway | Learn cloud deployment, ~$30/mo |

**Why this stack?**
- Portfolio-friendly (in-demand skills)
- Single C# codebase reduces complexity
- Scales from MVP to production
- PostGIS is purpose-built for location search

See `/docs/02_Requirements_And_Planning.md` → **Section: Tech Stack Selection** for detailed rationale (I evaluated 4+ alternatives).

---

## 📊 Project Philosophy: "Leave Room For"

**Key architectural principle:** Structure the codebase to support future features WITHOUT building them in MVP.

**Why?** Shows employers I can:
- Plan for scale without over-engineering
- Balance scope vs. quality
- Think about maintainability

**Examples:**
- Empty `/docs/future-features/` folder with specs for admin panel, notifications, analytics (not built, but planned)
- Database schema includes future tables (commented out in migrations)
- API has stubbed endpoints for v2 features
- Logging infrastructure captures data we'll analyze later

This is how real teams work—ship MVP, learn, iterate. Not "build everything upfront."

---

## 🗺️ Current Roadmap

### ✅ Phase 1: Discovery (Complete)
Market validation, user research, tech decisions → [See docs/01_Discovery.md](docs/01_Discovery.md)

### ✅ Phase 2: Planning (Complete)
Database schema, API design, 8-week milestone breakdown → [See docs/02_Requirements_And_Planning.md](docs/02_Requirements_And_Planning.md)

### 🔄 Phase 3: Legal Setup (In Progress)
LLC structure, Terms/Privacy Policy → [See docs/03_Legal_Business_Setup.md](docs/03_Legal_Business_Setup.md)

### 🚧 Phase 4: MVP Development (Next — 8 weeks)
- **M1:** Solution setup, migrations, hello-world deploy (Week 1)
- **M2:** Core search logic, distance calculations (Week 2-3)
- **M3:** MAUI UI, search/results pages (Week 3-4)
- **M4:** Maps integration (Week 5)
- **M5:** Admin CSV tools (Week 6)
- **M6:** Polish, deploy, beta test (Week 7-8)

### 📅 Phase 5: Launch (Week 9-10)
Beta with 5-10 practitioners, app store submission, union outreach

### 🔮 Future (If Successful)
See `/docs/future-features/` for detailed specs on admin panel, push notifications, analytics, multi-state expansion.

---

## 📁 Repository Structure
```
EDHSFinder/
├── docs/
│   ├── 01_Discovery.md              # ⭐ Start here — problem validation
│   ├── 02_Requirements_And_Planning.md  # ⭐ Then here — technical specs
│   ├── 03_Legal_Business_Setup.md
│   └── future-features/             # Planned but not built (shows foresight)
├── src/
│   ├── EDHSFinder.API/              # ASP.NET Core backend (not built yet)
│   ├── EDHSFinder.Core/             # Shared domain logic
│   └── EDHSFinder.MAUI/             # Frontend (mobile + web)
├── tests/
└── scripts/
```

**👀 For hiring managers:** Check out the `/docs` folder first. The planning process is more impressive than the code (for now).

---

## 🚀 Quick Start (Once Code Exists)
```bash
# Clone
git clone https://github.com/sooperD00/EDHSFinder.git
cd EDHSFinder

# Setup database (PostgreSQL + PostGIS)
docker run --name edhsfinder-db -e POSTGRES_PASSWORD=dev -p 5432:5432 -d postgis/postgis:15-3.3

# Run migrations
cd src/EDHSFinder.API
dotnet ef database update

# Start API
dotnet run  # Runs on https://localhost:5001

# Run MAUI app (separate terminal)
cd src/EDHSFinder.MAUI
dotnet build -t:Run -f net8.0-android  # Or net8.0-ios, net8.0-windows
```

*(Not yet implemented—check back in 2 weeks)*

---

## 🤔 Why Build This in Public?

**Transparency over secrecy.** My competitive advantages are:
1. Partner's union network (can't clone that)
2. Domain knowledge (I've done the research)
3. Actually shipping it (most people don't finish)

The code itself isn't proprietary—it's solid architecture applied to a real problem. **That's what employers want to see.**

If the business succeeds, great. If not, this is still a strong portfolio piece demonstrating senior-level thinking.

---

## 💬 Contact & Collaboration

- **Developer:** Nicole Rowsey
- **Email:** nicole.rowsey@gmail.com
- **LinkedIn:** [linkedin.com/in/nicolerowsey](https://linkedin.com/in/nicolerowsey) *(update with real link)*
- **Other Projects:** [github.com/sooperD00](https://github.com/sooperD00)

Currently job searching (senior IC / engineering manager roles in data/software). Open to discussing this project or others.

---

**Built with ❤️ and .NET** | [View Planning Docs](docs/) | [Future Features](docs/future-features/)