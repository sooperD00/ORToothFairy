# Legal & Business Setup Phase - EDHS Finder Project -> OR Tooth Fairy Project

**Phase Status:** Next Up  
**Previous Phase:** [Requirements & Planning](02_Requirements_And_Planning.md)  
**Next Phase:** Build MVP

---

## Overview

This phase covers:
1. Business structure (LLC or partnership)
2. Operating agreements & IP ownership
3. Liability & insurance considerations
4. Privacy & data protection compliance
5. Software licensing
6. Revenue sharing & monetization structure

---

## Business Model Deep Dive

### Revenue Streams
- **Primary:** Practitioner subscription ($15/month)
- **Future:** Lead generation fees, premium listings, other trade union apps

### Cost Structure
- **Fixed:** Hosting (~$20/month), domain ($12/year), app store fees ($99+$25 one-time)
- **Variable:** Map API calls (likely stays in free tier)
- **Time:** Developer maintenance (~2-5 hours/month estimated)

### Break-Even Analysis
- Monthly costs: ~$20-30
- Break-even: 2-3 paying practitioners
- Target: 10+ practitioners in first 6 months ($150/month revenue)

---

## Legal Structure Options

### Option 1: No Formal Entity (Simplest)
**Pros:** 
- Zero setup cost
- No paperwork
- Fast to start

**Cons:**
- No liability protection
- Personal assets at risk
- Unclear IP ownership
- Harder to split revenue

**Recommendation:** Only if staying <$1000/year revenue and treating as hobby.

---

### Option 2: Join Neighbor's Existing LLC
**Pros:**
- No new entity needed
- She already has business infrastructure
- Easier accounting

**Cons:**
- Complicates her union role (conflict of interest?)
- Less flexibility if partnership doesn't work
- IP ownership tied to her LLC

**Recommendation:** Ask lawyer husband about conflict of interest implications.

---

### Option 3: New LLC (50/50 or other split)
**Pros:**
- Clean liability protection
- Clear IP ownership
- Professional structure for future projects
- Easy to dissolve if needed

**Cons:**
- ~$100 Oregon filing fee
- Annual reporting requirements
- Separate bank account needed
- More complex taxes (but still simple for small business)

**Recommendation:** If you see this as first of multiple projects together, do this.

---

### Option 4: Developer as Contractor, Neighbor as Client
**Pros:**
- You keep IP, license it to her
- Clear boundaries
- Easy to walk away

**Cons:**
- Less "partnership" feel
- Revenue split unclear
- She takes all business risk

**Recommendation:** Only if you want to stay pure developer (not business partner).

---

## Key Questions for Lawyer Husband

- [ ] **Conflict of interest:** Can neighbor (as union ED) run competing/partnering app?
- [ ] **IP ownership:** If we split 50/50, who owns code if we dissolve?
- [ ] **Liability:** Could you be sued if app shows wrong contact info / patient has bad experience?
- [ ] **Insurance:** Do we need professional liability insurance? Errors & omissions?
- [ ] **Union relationship:** Any legal risk if union later claims ownership/right to data?
- [ ] **HIPAA:** Does this app touch PHI (Protected Health Information)? Probably not, but confirm.
- [ ] **Professional licensing:** Any Oregon laws about "referring" patients to healthcare providers?

---

## Liability & Insurance Considerations

### Potential Liability Scenarios
1. **Wrong contact info:** Patient can't reach practitioner, misses care
2. **Unlicensed practitioner:** Admin error, unqualified person listed
3. **Data breach:** Practitioner contact info leaked
4. **Discrimination:** Algorithm/search appears to discriminate by location/race/etc
5. **Medical outcome:** Patient unhappy with practitioner's care, blames app

### Risk Mitigation Strategies
- **Disclaimers:** "This is a directory only. Verify credentials independently."
- **No endorsement:** "Listing does not constitute endorsement."
- **License verification:** Admin manually checks official state records
- **No medical advice:** "Contact practitioner directly for medical questions."
- **Terms of Service:** Users agree we're not liable for practitioner's care

### Insurance Options
- **General liability:** ~$500/year (probably overkill for software)
- **Cyber liability:** ~$1000/year (for data breaches)
- **Errors & omissions (E&O):** ~$800/year (for software bugs causing harm)

**Recommendation:** Start with none (risk is low), add if revenue grows or lawyer recommends.

---

## Privacy & Data Protection

### Data We Collect

**From Practitioners:**
- Name, license #, contact info, location (they provide willingly)
- No sensitive health data

**From Patients:**
- Search location (temporary, for distance calculation)
- Device/browser info (for analytics)
- IP address (for rate limiting)

### Privacy Compliance Checklist

- [ ] **Privacy Policy:** Required, even for simple apps
  - What data we collect
  - How we use it
  - How long we keep it
  - Who we share with (nobody)
  - User rights (delete requests, etc.)

- [ ] **Terms of Service:** Protect us from liability
  - Disclaimer of warranties
  - Limitation of liability
  - User responsibilities
  - Dispute resolution

- [ ] **Cookie Consent:** If using analytics (Google Analytics, etc.)
  - EU: GDPR requires explicit consent
  - US: Less strict, but good practice

- [ ] **Data Retention:** How long to keep search logs?
  - Recommendation: 30 days for debugging, then anonymize or delete

- [ ] **Right to Delete:** Can practitioners remove their data?
  - Yes, admin can deactivate them anytime

### HIPAA Analysis (Preliminary)

**Is this a HIPAA-covered entity?** Probably not.

**Reasoning:**
- We don't transmit/store patient health records
- We don't bill insurance
- We're just a directory (like Yellow Pages)
- Contact happens outside our app

**BUT:** Confirm with lawyer. If Oregon has stricter health privacy laws, we might need to comply.

---

## Software Licensing

### Code You Write (Your IP)

**Options:**

**1. Closed Source (Recommended for now)**
- Code is private, not on GitHub publicly
- You and neighbor have access
- Protects competitive advantage
- Easy to open-source later if desired

**2. Open Source (MIT, Apache 2.0)**
- Good for portfolio visibility
- Anyone can copy/use
- Loses competitive advantage
- Weird if you're charging practitioners while code is free

**Recommendation:** Keep private repo during development. Make public (with open source license) if:
- You want to attract contributors
- Project fails and you want portfolio piece
- Revenue is strong and open-sourcing helps reputation

---

### Third-Party Dependencies

**Map APIs (Mapbox/Google):**
- Review terms: Can you use free tier for commercial app?
- Usually yes, but confirm

**.NET & Libraries:**
- Most are MIT or Apache licensed (free for commercial use)
- Check licenses: `dotnet list package --include-transitive`

**UI Components (MudBlazor, etc.):**
- Usually MIT (free), but double-check

---

## Operating Agreement (If Creating LLC)

### Key Provisions

**Ownership Split:**
- 50/50? Or weighted by contribution?
- Developer: Time, skills
- Neighbor: Domain expertise, customer access

**Revenue Split:**
- Same as ownership? Or different?
- Example: 60/40 (developer gets more for ongoing maintenance)

**Decision Making:**
- Unanimous for major changes (new features, pivots)
- Majority for operations (pricing, marketing)

**IP Ownership:**
- LLC owns code/brand
- If dissolved, developer can buy back IP (or it goes to auction)

**Exit Strategy:**
- Either party can leave with 30 days notice
- Leaving party forfeits ownership, keeps past revenue
- OR: Leaving party must sell shares to remaining party

**Time Commitment:**
- Developer: X hours/week (document expectations)
- Neighbor: Y hours/week (practitioner outreach, support)

**Dispute Resolution:**
- Mediation first, arbitration if needed (avoid lawsuits)

---

## Next Steps (This Phase)

1. **Consult lawyer husband** (~1 hour conversation)
   - Review questions above
   - Get recommendation on LLC vs contractor model
   - Draft simple operating agreement if LLC

2. **Talk to neighbor about business structure** (~30 min)
   - Discuss ownership split
   - Confirm her conflict of interest situation with union
   - Agree on revenue split

3. **File LLC paperwork (if going that route)** (~1 week)
   - Oregon LLC filing: online, ~$100
   - EIN from IRS: free, 15 minutes online
   - Bank account: ~1 hour at bank

4. **Draft Terms of Service + Privacy Policy** (~2 hours)
   - Use template (TermsFeed, Termly, etc.)
   - Customize for your app
   - Have lawyer review (optional but recommended)

5. **Set up bookkeeping** (~30 min)
   - Wave, QuickBooks, or simple spreadsheet
   - Track revenue, expenses, hours

---

## Open Questions for Discussion

- [ ] What ownership split feels fair? (50/50, 60/40, other?)
- [ ] Is neighbor's union role a conflict of interest?
- [ ] Do we need insurance from day 1, or wait until revenue covers it?
- [ ] How do we handle if one person stops contributing?
- [ ] What if a practitioner sues us? (Unlikely, but plan for worst case)

---

## Estimated Timeline for This Phase

- **If skipping LLC:** 1 week (just write Terms/Privacy, talk to lawyer)
- **If forming LLC:** 2-3 weeks (paperwork, bank account, agreements)

**Recommendation:** Don't let legal stuff delay coding too much. You can form LLC while building MVP.

---

**Status:** Waiting for legal consultation before finalizing structure.

**Next:** Once business stuff is sorted, move to Phase 4 (Build MVP) and start coding! 🚀
