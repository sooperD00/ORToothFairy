# Discovery Phase - EDHS Finder Project -> OR Tooth Fairy Project

**Date Started:** October 25, 2025  
**Status:** Complete  
**Next Phase:** Requirements & Planning

---

## Executive Summary

Building a location-based finder app for Expanded Practice Dental Hygienists (EDHS) in Oregon. The app connects patients who need dental care with independent EDHS practitioners who can provide services without dentist supervision in underserved settings (nursing homes, low-income populations, homebound individuals).

**Core Problem:** Patients can't find EDHS practitioners (Google doesn't work, no directory exists). Current discovery method is word-of-mouth only.

**Solution:** Mobile-responsive web app + native mobile apps that allow patients to search by location and contact practitioners directly.

**Market Size:** ~400 EDHS practitioners in Oregon (300 in local union)

**Business Model:** Practitioners pay $15/month subscription to be listed

**Timeline:** 2-3 months for MVP (80-120 hours)

**Budget:** ~$15-30/month hosting costs split between partners

---

## Stakeholders

### Primary Partner
- **Neighbor:** Part-time Executive Director of dental hygienist trade union
- **Role:** Domain expert, practitioner liaison, business/marketing lead
- **Contribution:** Practitioner outreach, license verification, handles support
- **Availability:** Variable (she works a lot, different hours than developer)

### Developer (You)
- **Role:** Technical lead, architect, sole developer
- **Goals:** Portfolio piece, learn modern .NET stack, test partnership viability, potential side income
- **Constraints:** Young kids (availability varies), job searching (may need to pause)

### End Users (3 profiles)

**1. Patients (Primary User)**
- Looking for dental care in their area
- Often underserved populations (low-income, elderly, homebound)
- Medicare/Medicaid typically covers services
- May not know EDHS services exist

**2. Practitioners (Customers)**
- 400 total in Oregon
- Make good money providing valuable service
- "So behind" on technology
- Need patient pipeline

**3. Admins (You + Neighbor)**
- Manual verification against official state spreadsheet
- Approve/reject applications
- Need basic stats/governance data

---

## Current State Analysis

### How Patients Currently Find EDHS
- **Word of mouth only**
- Google search fails ("EDHS" autocorrects to "ODHS" - unrelated)
- No union directory available to public
- **Result:** Many potential patients don't know service exists

### Market Context
- **Union Dynamics:** Local union pays 60% dues to "badly run" national union (lost money recently)
- Many practitioners want alternatives to union membership
- Unclear if union will partner, compete, or be neutral
- Neighbor (as union ED) can navigate this politically

### Competitive Landscape
- No competing solution exists
- "They are so behind" technologically
- Ample opportunity, no urgency (nobody else working on this)

---

## MVP Feature Set (Validated)

### Patient Features
- **Search by location** (3 input methods):
  - Geolocation (auto-detect user's location)
  - Zip code (e.g., adult child searching for parent's area)
  - Street address
- **Distance filtering:** 5 miles, 10 miles, 20 miles, or "all"
- **Results display:** 
  - Name
  - Services offered
  - Contact method (phone/email displayed directly)
- **Load reasonable number of results** (paginate if needed)

### Practitioner Features (Deferred for MVP)
- ~~Advertising availability~~ (future iteration if practitioners will pay for it)
- ~~In-app messaging~~ (too complex for MVP)
- Simple profile: contact info they're willing to share

### Admin Features (Simplified for MVP)
- **Manual data management:** Admin updates CSV/JSON file directly (no admin panel in v1)
- **License verification:** Cross-reference against official Oregon Board of Dentistry spreadsheet
- **Approval workflow:** Email-based for v1 (practitioner emails, admin verifies, admin adds to file)
- ~~Stats dashboard~~ (structure data to add later, but don't build UI yet)

---

## Technical Decisions

### Platform Strategy
**Decision:** Build .NET MAUI app from the start (web + native iOS/Android from single codebase)

**Reasoning:**
- Neighbor "really wants" native mobile apps
- Developer wants modern .NET experience for portfolio
- Flexible timeline allows for slightly longer initial build
- Avoids rebuild/refactor later
- MAUI is resume-worthy (newer tech, competitive advantage)

**Trade-off:** 80-120 hours vs 60-80 for web-only, but gets native apps immediately

### Tech Stack (Full .NET)

**Backend:** ASP.NET Core Web API
- RESTful API
- Entity Framework Core for ORM
- Jobs want this, developer has experience
- Enterprise-grade, scales well

**Frontend/Mobile:** .NET MAUI with Blazor Hybrid
- Single C# codebase → web, iOS, Android
- Blazor = HTML/CSS/C# (no JavaScript required)
- Built-in geolocation, maps support
- Modern (2022+), great for portfolio

**Database:** PostgreSQL
- PostGIS extension for geo queries ("find practitioners within X miles")
- Hosted on Railway or Supabase (free tier available)
- Developer comfortable with SQL

**Maps/Geolocation:**
- Mapbox (50k requests/month free) OR Google Maps (28k map loads/month free)
- Browser geolocation API for user's location (free, built-in)
- Haversine formula or PostGIS for distance calculations

**Hosting:**
- Backend API: Azure App Service (free tier → $15-30/month)
- Database: Railway/Supabase free tier
- Native apps: iOS App Store ($99/year) + Google Play ($25 one-time)

**Estimated Costs:** $15-30/month after free tiers (acceptable to both partners)

### Deployment Strategy
**Phase 1:** Deploy as URL neighbor can share (mobile-responsive web)  
**Phase 2:** Publish to app stores (iOS/Android native)

Rationale: Get something in her hands fast, validate with users, then add polish.

---

## Scope & Constraints

### In Scope (MVP)
- Oregon only (EDHS is state-specific certification)
- 400 practitioners max (small dataset, manageable manually)
- Direct contact display (no in-app messaging)
- Manual admin updates (no admin panel)
- Mobile-responsive web + native apps

### Out of Scope (Future Iterations)
- Admin panel UI (build foundation, but manual for v1)
- Stats dashboard (log data, don't display yet)
- Practitioner availability advertising
- In-app messaging/notifications
- Other states (Alaska mentioned as potential expansion)
- Other trade unions (neighbor has contacts in construction, etc.)

### Technical "Leave Room For" (Foundation Planning)
These won't be built in MVP but will be architecturally supported:
- **User roles in database** (patient, practitioner, admin) - even if only admins exist in v1
- **Event logging** (search queries, clicks, contact attempts) - log from day 1, display later
- **Notification system structure** (design API to support, don't implement)
- **Admin panel routes** (stub out controllers, leave commented)

**Portfolio Note:** Empty folders with README files explaining "future iteration" show good planning to employers.

---

## Risks & Mitigations

### Partnership Risk
**Risk:** Neighbor too busy to contribute business side  
**Mitigation:** Treat as portfolio piece first, revenue second. Developer happy with $15/mo passive income even if it doesn't scale.

### Union Political Risk
**Risk:** Union sees app as competition, creates drama  
**Mitigation:** Neighbor (as ED) handles politics. App is useful regardless of union relationship.

### Time/Availability Risk
**Risk:** Developer's job search or family needs interrupt timeline  
**Mitigation:** No hard deadline. Deploy v1 as soon as basic search works. Can pause/resume anytime.

### Technical Complexity Risk
**Risk:** MAUI is newer, fewer tutorials available  
**Mitigation:** Developer comfortable figuring things out. Can fall back to Blazor Server if MAUI blocks progress.

### Market Risk
**Risk:** Practitioners won't pay, patients won't use  
**Mitigation:** Low financial risk ($15/mo each). Test with neighbor's union contacts first.

---

## Success Criteria

### Minimum Viable Success (Portfolio Win)
- Working app deployed and accessible
- GitHub repo with clean architecture
- Resume bullet point: "Built cross-platform healthcare finder app with .NET MAUI"

### Business Success (If Partnership Works)
- 10+ practitioners paying $15/month ($150/mo revenue)
- Covers hosting costs + modest side income
- Validates partnership for future projects

### Expansion Success (Dream Scenario)
- 50+ practitioners ($750/mo)
- Expand to other states (Alaska, others with EDHS)
- Other trade union apps (neighbor's contacts)

---

## Key Learnings from Discovery

1. **Market fit is real** - clear pain point, no existing solution, willing customers
2. **Scope is manageable** - small dataset (400 practitioners), simple workflow
3. **Technical risk is acceptable** - modern stack, flexible timeline, can simplify if needed
4. **Business risk is low** - portfolio value regardless, modest financial commitment
5. **Partnership is unproven** - intentionally treating as test, easy to walk away

---

## Next Steps
1. ✅ Complete discovery documentation (this file)
2. → Move to Requirements & Planning phase
3. Define exact data model (practitioner fields, search parameters)
4. Create technical architecture diagram
5. Set up project structure with "leave room for" scaffolding
6. Define development milestones

---

## Questions for Neighbor (Before Requirements Phase)

- [ ] Union relationship: Will they actively promote this? Compete? Neutral?
- [ ] Practitioner data collection: What specific fields are they willing to share?
- [ ] License verification: How often? Who does annual re-checks?
- [ ] Conflict of interest: How does she manage being ED while building competitor/partner?
- [ ] Initial outreach: Can she send "who's interested" survey before we build?

---

**Decision:** Proceed to Requirements & Planning phase with .NET MAUI stack.
