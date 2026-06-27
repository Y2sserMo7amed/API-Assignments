# ECommerceApp — Session 01 + Session 02

My own version of the project, following the same topics taught in class.
I'm a beginner, so I kept the code simple and added comments explaining
*why* each piece exists. Where the instructor used a shortcut I haven't
learned yet (like C# 12 primary constructors), I wrote it the "long way"
instead.

## Session 01 recap

- What an API/endpoint is, API types (open/internal), API styles (REST/SOAP/GraphQL)
- Onion Architecture (Domain → Application → Infrastructure → API, dependencies point inward only)
- The 4 projects, `BaseEntity<TKey>`, the `Product`/`ProductBrand`/`ProductType` entities
- `StoreDbContext` + first migration (`InitialCreate`) + first real database

## Session 02 — what I added

### 1. Data Seeding
- `ECommerceApp.API/Data/SeedData/brands.json`, `types.json`, `products.json`
  (the files given for this lesson)
- `ECommerceApp.Infrastructure/Data/StoreContextSeed.cs` — reads those JSON
  files and inserts them, but only if the tables are still empty. Brands and
  Types are seeded *before* Products (Products need their Ids as FKs).
- Called once at startup from `Program.cs`.

### 2. Generic Repository
- `ECommerceApp.Domain/Contracts/IGenericRepository.cs` — one interface
  (GetById, GetAll, Add, Update, Delete) reused by every entity.
- `ECommerceApp.Infrastructure/Repositories/GenericRepository.cs` — the EF
  Core implementation, using `dbContext.Set<TEntity>()`.

### 3. Unit of Work
- `ECommerceApp.Domain/Contracts/IUnitOfWork.cs` — hands out a repository
  for whichever entity you ask for, plus one `SaveChangesAsync()`.
- `ECommerceApp.Infrastructure/Repositories/UnitOfWork.cs` — caches one
  repository per entity type and saves everything through one `DbContext`.
- Registered in `InfrastructureServicesRegistration.cs`.

### 4. Product Module — DTOs, Mapping, Service, Controller
- `Product.cs` now has `BrandId`/`Brand` and `TypeId`/`Type` (FK + navigation
  property). EF Core wires the relationship up automatically by convention -
  no Fluent API needed for this.
- `ECommerceApp.Application/DTOs/` — `ProductDto`, `BrandDto`, `TypeDto`
  (the flat shapes returned to the client).
- `ECommerceApp.Application/Mapping/MappingProfile.cs` — AutoMapper rules:
  entity → DTO.
- `ECommerceApp.Application/Services/` — `IProductService` / `ProductService`,
  the 4 read operations (GetAllProducts, GetProductById, GetAllBrands, GetAllTypes).
- `ECommerceApp.API/Controllers/ProductsController.cs` — the 4 real endpoints:

  | Verb | Route | Action |
  |---|---|---|
  | GET | `/api/products` | GetAllProducts() |
  | GET | `/api/products/{id}` | GetProduct(id) → 404 if not found |
  | GET | `/api/products/brands` | GetAllBrands() |
  | GET | `/api/products/types` | GetAllTypes() |

- Removed the default `WeatherForecast.cs` + `WeatherForecastController.cs`
  template files - the real endpoints replace them now.
- `ApplicationServicesRegistration.cs` (new) - registers AutoMapper + `IProductService`,
  called from `Program.cs` alongside `AddInfrastructureServices()`.

### 5. Picture URL Resolver
- `ECommerceApp.Application/Mapping/PictureUrlResolver.cs` - turns the
  relative path stored in the DB (`images/products/shoe.jpg`) into a full
  URL using a `BaseUrl` setting from `appsettings.json`, so it works in any
  environment without code changes.
- `wwwroot/images/products/` (new) - the actual product images, served via
  `app.UseStaticFiles()` (added in `Program.cs`).

## A couple of things I simplified on purpose

- **No primary constructors.** The instructor wrote `GenericRepository` and
  `UnitOfWork` using C# 12's primary-constructor shorthand
  (`class Foo(Bar bar) : IFoo`). I wrote normal constructors instead -
  same result, just a syntax I already understand.
- **No custom Result/Error pattern.** I didn't build a full `Result<T>`/
  `Error` type system. Instead I used the simple, standard ASP.NET Core
  approach: `GetProductByIdAsync` returns `null` when not found, and the
  controller turns that into `NotFound()`. Same outcome (a 404), much less
  new stuff to learn at once.

## ⚠️ A known bug I'm leaving in on purpose

`GenericRepository.GetAllAsync()` / `GetByIdAsync()` do **not** `.Include()`
the `Brand`/`Type` navigation properties. That means right now, calling
`GET /api/products` or `GET /api/products/{id}` will likely throw a
`NullReferenceException` (500 error) when AutoMapper tries to read
`src.Brand.Name` / `src.Type.Name` on a product whose `Brand`/`Type` came
back `null`.

This isn't a mistake - the lesson calls this out explicitly as "the problem
the Specification Pattern solves," and that's planned for a future session.
`GET /api/products/brands` and `GET /api/products/types` should work fine
right now since they don't need any `.Include()`.

## Before you run it

1. **New migration needed.** `Product` now has `BrandId`/`TypeId` columns
   that didn't exist in the first migration. In Package Manager Console
   (Default project = `ECommerceApp.Infrastructure`):
   ```
   Add-Migration AddProductBrandAndType -Project ECommerceApp.Infrastructure -StartupProject ECommerceApp.API
   Update-Database -Project ECommerceApp.Infrastructure -StartupProject ECommerceApp.API
   ```
2. Run the app - the first request will trigger seeding (brands, types,
   then 13 products).
3. Try `GET /api/products/brands` and `GET /api/products/types` first -
   those should just work. `GET /api/products` will likely 500 (see the bug
   note above) until the Specification Pattern lesson fixes `.Include()`.

## Next session (what I expect to add)

- Specification Pattern - fixes the `.Include()` / null navigation problem
- Maybe pagination, filtering, sorting on `GET /api/products`
- POST / PUT / DELETE endpoints (full CRUD)
