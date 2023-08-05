
using AdvertisementApp.Business.DependencyResolvers.Microsoft;
using AdvertisementApp.Business.Helpers;
using AdvertisementApp.UI.Mappings.AutoMapper;
using AdvertisementApp.UI.Models;
using AdvertisementApp.UI.ValidationRules;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var profiles = ProfileHelper.GetProfiles();
profiles.Add(new UserCreateModelProfile());

var configuraiton = new MapperConfiguration(opt =>
{
    opt.AddProfiles(profiles);
});
var mapper = configuraiton.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // cookie ekleme ve configure etme
    .AddCookie(opt=>
    {
        opt.Cookie.Name = "AtillaCookie";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; // cookie paylaþýlamaz
        opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest; // http ile gelirse kullancý http ile devam et. https ile gelirse https ile devam et
        opt.ExpireTimeSpan = TimeSpan.FromDays(20);
        opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn"); // cookie nin login baþlangýç sayfasýný belirliyoruz
        opt.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/LogOut");
        opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/AccessDenied");



    });

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

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
