
using AdvertisementApp.Business.DependencyResolvers.Microsoft;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Business.ValidationRules;
using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dto;
using AdvertisementApp.UI.Models;
using AdvertisementApp.UI.ValidationRules;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
