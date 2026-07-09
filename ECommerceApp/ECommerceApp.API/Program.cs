using ECommerceApp.API.Extensions;
using ECommerceApp.Application;
using ECommerceApp.Infrastructure;
using ECommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

await app.SeedDatabaseAsync();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
