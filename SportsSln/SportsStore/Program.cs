using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});
builder.Services.AddScoped<IStoreRepository<Product>, EFStoreRepository>();
var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute("pagination", "Products/page{productPage}", new { Controller = "Home", Action = "Index" });
//app.MapControllerRoute("pagination",
//"Products/Page{productPage}",
//new { Controller = "Home", action = "Index" });
app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);

app.Run();
