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
app.MapControllerRoute("default",
    "{Controller}/{Index}",
    new { Controller = "Home", Action = "Index" });

app.MapControllerRoute("pageCat",
    "{category}/Page{productPage:int}", 
    new { Controller = "Home", Action = "Index"});

app.MapControllerRoute("page", 
    "page{productPage:int}", 
    new { Controller = "Home", Action = "Index" , productPage = 1 });

app.MapControllerRoute("category",
    "{category}",
    new { Controller = "Home", Action = "Index", productPage = 1 });

app.MapControllerRoute("pagination",
    "Products/page{productPage:int}",
    new { Controller = "Home", Action = "Index" });

app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);

app.Run();
