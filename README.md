Yo wsg twin 👀🫂
[

![Deploy to Render](https://render.com/images/deploy-to-render-button.svg)

](https://render.com/deploy?repo=https://github.com/ZieerHans/Website-making-101-The-Example)

# Echoes of Grace — Minimalist Rebuild

Clean, minimal ASP.NET Core Razor Pages site. No book/parchment theme —
plain, modern, believable-as-hand-built design.

## What this site is
A Theology project ("A Theology PETA," Academic A-02, Group 2): a digital
book of personal confessions/testimonies inspired by St. Augustine, with
an introduction, ten individual confessions, a group reflection, a final
prayer/vow, and a live form where visitors can submit their own reflection.

## How it works, page by page
- **Home (`/`)** — hero, introduction text, dedication, and a table-of-contents
  preview linking into the confessions.
- **Table of Contents (`/TableOfContents`)** — full list of every section,
  linking to its anchor on the Confessions page or its own page.
- **Confessions (`/Confessions`)** — all ten confessions plus the group
  reflection on one page, each in its own `<article>`, with a sticky
  jump-nav that highlights the section currently in view as you scroll.
- **Final Prayer (`/FinalPrayer`)** — the closing prayer/vow, its own page,
  with a signed-off quote at the bottom.
- **Leave a Reflection (`/Reflection`)** — a real form. Submitting it does
  a genuine HTTP POST handled server-side (see below) — nothing here is
  faked in JavaScript.
- **Reflections (`/MyReflections`)** — lists every reflection submitted so
  far, proving the backend is actually storing and returning data rather
  than just showing a "thanks" message.

## How the reflection form actually works (server-side)
1. The browser submits a normal HTML `<form method="post">` — no JS
   intercepting it.
2. ASP.NET Core model-binds the POST body into a `ReflectionForm` object.
3. `Models/ReflectionForm.cs` has data-annotation rules (`[Required]`,
   `[MinLength]`, `[StringLength]`) that validate the submission
   server-side. If it fails, the page re-renders with error messages —
   still entirely server-driven.
4. If valid, `Pages/Reflection.cshtml.cs` calls into `Data/ReflectionStore.cs`,
   a singleton service that holds submissions in memory for the lifetime
   of the running server process, shared across every visitor.
5. `/MyReflections` reads from that same singleton and renders the full
   list back out as HTML.

**Important limitation:** the store is in-memory only, not a database.
Render's free tier can spin the service down when idle, which clears the
list on the next cold start. That's fine for demonstrating the real
request → validate → store → retrieve pipeline; it isn't durable
long-term storage. To make submissions survive restarts, this would need
a persisted disk or a small external database.

We originally emailed each submission via Gmail SMTP, but Render's free
tier blocks outbound SMTP ports (25/465/587), so that approach silently
failed in production even though it worked locally. The in-memory store
replaced it entirely — no email credentials are used anywhere now.

## Structure
- `Program.cs` — startup; binds to `0.0.0.0:$PORT` (works on Replit and
  Render/Docker); registers `ReflectionStore` as a singleton.
- `Dockerfile` — used by Render to build and run the app.
- `Data/ContentData.cs` — C# model + all 13 content entries (introduction,
  10 confessions, group reflection, final vow), transcribed verbatim.
- `Data/ReflectionStore.cs` — in-memory, thread-safe store for submitted
  reflections; see explanation above.
- `Models/ReflectionForm.cs` — server-side validation rules for the form.
- `Pages/Shared/_Layout.cshtml` — shared HTML shell: viewport meta, nav
  (with mobile hamburger toggle), footer.
- `Pages/Index.cshtml(.cs)` — Home: hero, introduction, table of contents.
- `Pages/Confessions.cshtml(.cs)` — the ten confessions + group reflection
  on one page with a sticky jump-nav, anchor-linked.
- `Pages/FinalPrayer.cshtml(.cs)` — the closing prayer/vow, its own page.
- `Pages/Reflection.cshtml(.cs)` — the reflection form and its server-side
  handling, described above.
- `Pages/MyReflections.cshtml(.cs)` — lists every reflection saved so far.
- `wwwroot/css/site.css` — minimalist styling: off-white background, one
  accent color, serif headings / sans body, no textures or 3D.
- `wwwroot/js/site.js` — small, honest amount of vanilla JS: mobile nav
  toggle, and scroll-based active-section highlighting on the Confessions
  jump-nav. No form logic lives here — the reflection form is handled
  entirely server-side.

## Run
