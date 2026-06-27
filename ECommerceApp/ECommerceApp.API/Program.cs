using ECommerceApp.Application;
using ECommerceApp.Infrastructure;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register our DbContext so EF Core knows how to connect to SQL Server.
// The connection string comes from appsettings.json ("DefaultConnection").
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// This is the Composition Root - we now call BOTH layers' registration methods.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Lets the app serve files from wwwroot (our product images) as plain URLs,
// e.g. https://localhost:7001/images/products/CottonHoodie.jpg
app.UseStaticFiles();

// Load our starter data (brands, types, products) into the database,
// but only the first time - if the tables already have rows, this does nothing.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    await StoreContextSeed.SeedAsync(dbContext, app.Environment.ContentRootPath);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
