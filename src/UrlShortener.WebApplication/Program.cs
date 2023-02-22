using Microsoft.EntityFrameworkCore;
using UrlShortener.Data.Data;
using UrlShortener.Data.Repositories;
using UrlShortener.Domain.Interfaces;
using UrlShortener.WebApplication.Services.Url;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApiContext>(
    options => options.UseInMemoryDatabase(databaseName: "InMemoryDatabase"));

builder.Services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
builder.Services.AddScoped<IUrlService, UrlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
