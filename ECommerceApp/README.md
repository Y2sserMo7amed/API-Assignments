# ECommerceApp — Session 01 + 02 + 03

My own version of the project, following the same topics taught in class.
I'm a beginner, so I kept the code simple and added comments explaining
*why* each piece exists.

## Session 01 recap
Onion Architecture (4 projects), `BaseEntity<TKey>`, the `Product`/
`ProductBrand`/`ProductType` entities, `StoreDbContext`, first migration
+ first database.

## Session 02 recap
Data seeding from JSON, Generic Repository + Unit of Work, DTOs +
AutoMapper, the Product Service + Controller (4 endpoints), the Picture
URL Resolver. Left a known bug in on purpose: `GetAllAsync()` didn't
`.Include()` Brand/Type, so `/api/products` threw a 500 error.

## Session 03 — what I added (this is the session that fixes that bug!)

### 1. The Specification Pattern
- `ECommerceApp.Domain/Specifications/BaseSpecification.cs` — a reusable
  base class with `Criteria` (the Where condition), `Includes` (related
  data to load), `OrderBy`/`OrderByDescending`, and `Skip`/`Take` for paging.
  One object describes the whole query instead of writing
  `.Where().Include().OrderBy()` by hand every time.
- `ECommerceApp.Infrastructure/Specifications/SpecificationEvaluator.cs` —
  reads a Specification and turns it into a real EF Core query.
- `IGenericRepository`/`GenericRepository` got 3 new methods:
  `GetEntityWithSpecAsync`, `GetAllWithSpecAsync`, `CountAsync` (the last
  one ignores paging — it's for the pagination "total count" field).

  > ⚠️ **Naming note:** the slides write `p.ProductBrand`/`p.ProductType` in
  > their `.Include()` examples, but **our actual `Product` entity** (built
  > in Session 02) names these navigation properties `Brand`/`Type` instead.
  > I kept *our* real names so nothing breaks — same idea, just our naming.

### 2. The concrete Product specification
- `ECommerceApp.Application/Specifications/ProductsWithBrandsAndTypesSpecification.cs`
  — has two constructors:
  - One for **Get All Products**: builds a single filter condition that
    handles "filter by brand", "filter by type", "filter by both", and
    "search by name" all at once (if a filter wasn't supplied, that part
    just matches everything), plus sorting and paging.
  - One for **Get Product By Id**: just `Id == id`, with Brand/Type included.
- `ECommerceApp.Application/DTOs/ProductSpecParams.cs` (new) — the filter/
  search/sort/page options that come in from the query string
  (`?brandId=2&search=jacket&sort=priceDesc&pageIndex=1&pageSize=6`).

### 3. Sorting
Handled inside the specification — `sort=priceAsc` / `sort=priceDesc` /
anything else defaults to sorting by `Name`.

### 4. Pagination + the Standard Response
- `ECommerceApp.Application/DTOs/PaginationResponse.cs` (new) — the exact
  shape shown in the lesson:
  ```json
  { "pageIndex": 1, "pageSize": 6, "count": 13, "data": [ ... ] }
  ```
  `count` is the total number of matching rows across *all* pages, not
  just the current page — that's why `CountAsync` ignores paging.
- `ProductService.GetAllProductsAsync` now takes a `ProductSpecParams` and
  returns a `PaginationResponse<ProductDto>` instead of a plain list.
- `ProductsController.GetAllProducts` now reads `[FromQuery] ProductSpecParams`
  so all the filter/search/sort/page options come straight from the URL.

### ✅ This fixes the Session 02 bug
`GetAllProductsAsync` and `GetProductByIdAsync` now use the specification,
which always `.Include()`s `Brand` and `Type`. So `src.Brand.Name` /
`src.Type.Name` in the mapping profile are no longer null — `/api/products`
and `/api/products/{id}` should both return real data now instead of a
500 error.

## What I simplified vs. the instructor's code
- Used `p.Brand`/`p.Type` (our real navigation property names) instead of
  the slide's `p.ProductBrand`/`p.ProductType`.
- One combined `Criteria` expression handles brand filter + type filter +
  search together, rather than separate specification classes for each
  combination — fewer classes to keep track of, same result.
- No max-page-size clamping on `ProductSpecParams.PageSize` — real apps
  often cap this (so nobody requests `pageSize=999999`), but I skipped it
  to keep the DTO simple. Easy to add later if you want.

## No new migration needed this session
Nothing changed about the database schema — the Specification Pattern is
purely about *how we query* the data that's already there. You can just
build and run.

## Quick things to test (Swagger or the `.http` file)
- `GET /api/products` → should now return real data (Brand/Type names
  filled in), paginated 6-at-a-time by default
- `GET /api/products?brandId=2`
- `GET /api/products?search=jacket`
- `GET /api/products?sort=priceDesc`
- `GET /api/products?pageIndex=2&pageSize=4`
- `GET /api/products/1` → single product, Brand/Type filled in

## Next session (what I expect to add)
- Maybe POST / PUT / DELETE (full CRUD) for products
- Possibly the same Specification approach applied to Brands/Types if they
  ever need filtering too
