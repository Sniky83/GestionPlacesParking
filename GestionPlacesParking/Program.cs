using GestionPlacesParking.Core.Application.Repositories;
using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Infrastructure.DataLayers;
using GestionPlacesParking.Core.Infrastructure.Web.Middlewares;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add context
string connectionString = builder.Configuration.GetConnectionString("ParkingContext");

builder.Services.AddDbContext<ParkingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Injections de dépendances
builder.Services.AddScoped<IUserDataLayer, SqlServerUserDataLayer>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Config session Login
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseRouting();

app.UseAuthorization();

app.UseSession();

// Implémentation des Custom Middlewares
app.UseRedirectIfNotConnected();

app.MapRazorPages();

app.Run();
