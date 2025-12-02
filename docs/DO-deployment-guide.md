# ORToothFairy → Digital Ocean Deployment

## Step 1: Add Dockerfile to your repo

Put the Dockerfile in your repo ROOT (same level as `src/`):

```
ORToothFairy/
├── Dockerfile        ← HERE
├── src/
│   ├── ORToothFairy.API/
│   ├── ORToothFairy.Core/
│   └── ORToothFairy.MAUI/
```

Then push:
```bash
git add Dockerfile
git commit -m "Add Dockerfile for DO deployment"
git push
```

## Step 2: Create Digital Ocean Account

1. Go to https://www.digitalocean.com
2. Sign up (they often have $200 free credit for new accounts)
3. Add payment method

## Step 3: Create App

1. Click "Create" → "Apps"
2. Select "GitHub" → Authorize → Select `sooperD00/ORToothFairy`
3. Branch: `main`
4. DO should auto-detect the Dockerfile ✓

## Step 4: Configure Environment Variable

In the app setup, add this environment variable:

| Key | Value |
|-----|-------|
| `ConnectionStrings__DefaultConnection` | `Host=yamabiko.proxy.rlwy.net;Port=26981;Database=railway;Username=postgres;Password=YOUR_NEW_PASSWORD;SSL Mode=Require;Trust Server Certificate=true` |

⚠️ Use your NEW rotated Railway password!

(Double underscore `__` is intentional — that's how .NET reads nested config from env vars)

## Step 5: App Settings

- **Plan**: Basic ($5/mo) is fine for MVP
- **Region**: San Francisco (closest to Oregon)
- **Instance size**: Basic 512MB is enough to start

## Step 6: Deploy

Click "Create Resources" and wait ~5 minutes.

You'll get a URL like: `https://ortoothfairy-api-xxxxx.ondigitalocean.app`

## Step 7: Test It

```bash
curl https://YOUR-APP-URL/api/practitioners/search?zipCode=97124&maxDistanceMiles=25
```

Should return JSON with practitioners!

---

## Next: Blazor Web Frontend

Once the API is live, we'll:
1. Create a new Blazor WASM project
2. Point it at your DO API
3. Deploy that too (same process)
4. Buy `ortoothfairy.com` and point it there

---

## Costs Summary

| Service | Cost |
|---------|------|
| DO App Platform (API) | ~$5/mo |
| DO App Platform (Web) | ~$5/mo (or free static tier) |
| Railway PostgreSQL | Free tier, then ~$5/mo |
| Domain (.com) | ~$12/year |
| **Total MVP** | **~$15-20/mo** |
