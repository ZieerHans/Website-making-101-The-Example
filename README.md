# Echoes of Grace — Minimalist Rebuild

Clean, minimal ASP.NET Core Razor Pages site. No book/parchment theme —
plain, modern, believable-as-hand-built design.

## Structure
- `Program.cs` — startup; binds to `0.0.0.0:$PORT` (works on Replit and Render/Docker).
- `Dockerfile` — used by Render to build and run the app.
- `Data/ContentData.cs` — C# model + all 13 content entries (introduction,
  10 confessions, group reflection, final vow), transcribed verbatim.
- `Models/ReflectionForm.cs` — server-side validation rules.
- `Pages/Shared/_Layout.cshtml` — shared HTML shell: viewport meta, nav
  (with mobile hamburger toggle), footer.
- `Pages/Index.cshtml(.cs)` — Home: hero, introduction, table of contents.
- `Pages/Confessions.cshtml(.cs)` — the ten confessions + group reflection
  on one page with a sticky jump-nav, anchor-linked.
- `Pages/FinalPrayer.cshtml(.cs)` — the closing prayer/vow, its own page.
- `Pages/Reflection.cshtml(.cs)` — the reflection form. Real HTTP POST,
  handled by `OnPost()` in C#: ASP.NET Core model-binds the submission,
  `ReflectionForm`'s data annotations validate it, and the page re-renders
  server-side with errors or the confirmation. Nothing faked in JS.
- `wwwroot/css/site.css` — minimalist styling: off-white background, one
  accent color, serif headings / sans body, no textures or 3D.
- `wwwroot/js/site.js` — small, honest amount of vanilla JS: mobile nav
  toggle, and scroll-based active-section highlighting on the Confessions
  jump-nav.

## Run
```
dotnet restore
dotnet run
```
Visit `/`, `/Confessions`, `/Reflection`.

## Deploy (Render)
1. Push this repo to GitHub.
2. On Render.com: New → Web Service → connect this repo.
3. Render auto-detects the `Dockerfile` — no other config needed.
4. Free tier, no credit card, no injected badge.

## For your presentation
- **HTML** → `.cshtml` Razor markup.
- **CSS** → `wwwroot/css/site.css`.
- **JavaScript** → `wwwroot/js/site.js` (nav toggle, scroll highlighting).
- **C# & ASP.NET** → `Program.cs`, `Pages/*.cshtml.cs`, `Data/ContentData.cs`,
  `Models/ReflectionForm.cs` — routing, content model, and the reflection
  form's validation/processing/confirmation round-trip.
