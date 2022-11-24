using GestionPlacesParking.Core.Application.Repositories;
using GestionPlacesParking.Core.Global.EnvironmentVariables.Envs;
using GestionPlacesParking.Core.Infrastructure.Databases;
using GestionPlacesParking.Core.Infrastructure.DataLayers;
using GestionPlacesParking.Core.Interfaces.Infrastructures;
using GestionPlacesParking.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string connectionString = ConnectionStringEnv.ConnectionString;

// Add context
builder.Services.AddDbContext<ParkingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Injections de d�pendances
builder.Services.AddScoped<IUserDataLayer, SqlServerUserDataLayer>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IParkingSlotDataLayer, SqlServerParkingSlotDataLayer>();
builder.Services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();

builder.Services.AddScoped<IReservationDataLayer, SqlServerReservationDataLayer>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<IDayRepository, DayRepository>();

// Config de la session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Ajout d'un Token CSRF
builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "XSRF-TOKEN";
    options.HeaderName = "XSRF-TOKEN";
    options.SuppressXFrameOptionsHeader = false;
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
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseRouting();

app.UseAuthorization();

app.UseSession();

// Impl�mentation des Customs Middlewares
//app.UseRedirectIfNotConnected();
//app.UseRedirectIfNotAdmin();

app.MapRazorPages();

app.Run();
