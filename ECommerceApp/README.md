# ECommerceApp — Session 01 (API Fundamentals + Onion Architecture)

This is my own version of the project built in Session 01, following the same
topics that were taught: API basics, Onion Architecture, and the start of the
Product module. I'm a beginner, so I kept everything as simple as possible
and added comments explaining *why* each piece exists.

## What I learned this session

1. **What an API is** — a contract that lets a client (browser/mobile/app)
   request data from a backend without knowing how the backend works
   internally. An **endpoint** is one URL + one HTTP verb (GET, POST, etc.).
2. **API types** — Open/Public (anyone can call it) vs Internal/Private
   (only inside the company).
3. **API styles** — REST (resource + HTTP verbs + JSON), SOAP (XML, strict
   WSDL contracts), GraphQL (one endpoint, client picks the fields it wants).
4. **Onion Architecture** — 4 layers, dependencies only point **inward**:
   - `Domain` (center) — entities + contracts. Depends on nothing.
   - `Application` — orchestrates use-cases. Depends on Domain only.
   - `Infrastructure` — talks to the database (and later: email, payments,
     etc.). Depends on Domain + Application.
   - `API` (outer edge) — controllers, `Program.cs` (the Composition Root).
     Depends on Application + Infrastructure.
5. **The Product module** — `Product`, `ProductBrand`, `ProductType`
   entities, all sharing a generic `BaseEntity<TKey>` for their `Id`.

## Project structure

```
ECommerceApp.sln
├── ECommerceApp.Domain/              (center of the onion - no references)
│   ├── Common/BaseEntity.cs
│   └── Entities/Products/
│       ├── Product.cs
│       ├── ProductBrand.cs
│       └── ProductType.cs
├── ECommerceApp.Application/         (references Domain - empty for now)
├── ECommerceApp.Infrastructure/      (references Domain + Application)
│   ├── Data/StoreDbContext.cs        (empty stub - finished next lesson)
│   └── InfrastructureServicesRegistration.cs
└── ECommerceApp.API/                 (references Application + Infrastructure)
    ├── Program.cs
    ├── Controllers/WeatherForecastController.cs   (default template, not removed yet)
    ├── WeatherForecast.cs
    └── appsettings.json
```

## What's intentionally NOT done yet

Following the lesson exactly, I stopped here on purpose:
- `StoreDbContext` doesn't inherit from `DbContext` yet — no `DbSet`s.
- No `ProductsController` / no real endpoints yet (those are listed as
  "endpoints we'll build" in the slides — future session).
- `Product` doesn't have `BrandId`/`TypeId` foreign keys yet — those come
  with entity configurations in the next lesson.
- No migrations, no entity configurations (Fluent API), no seeding yet.
- The default `WeatherForecast` template files are still there — I'll
  delete them once the real Product endpoints replace them.

## How to open it

1. Double-click `ECommerceApp.sln` to open it in Visual Studio 2022.
2. Set `ECommerceApp.API` as the Startup Project (right-click → "Set as
   Startup Project").
3. Press F5 / Run — Swagger should open in the browser showing the default
   `WeatherForecast` endpoint (the real Product endpoints aren't built yet).

## Next session (what I expect to add)

- Finish `StoreDbContext` (inherit `DbContext`, add `DbSet<Product>`, etc.)
- Add `IEntityTypeConfiguration<T>` classes for column rules + FK relationships
- Add a repository + `IUnitOfWork` (defined in Domain, implemented in
  Infrastructure)
- Wire up `AddInfrastructureServices()` in `Program.cs`
- Build the actual `GET /api/products` endpoints
