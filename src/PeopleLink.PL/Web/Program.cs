global using Web.Areas.Manager.Models;
global using Infrastructure.Data;
global using Infrastructure.Identity;
global using Web.Models;
global using Web.Areas.Employee.Models;
global using ApplicationCore.Entities;
global using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Specifications.LeaveSpecifications;
using ApplicationCore.Services;
using Web.Attributes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PeopleLinkContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PeopleLinkContext")));

builder.Services.AddDbContext<PeopleLinkIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PeopleLinkIdentityContext")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PeopleLinkIdentityContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<CheckIdentityNumber>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{Employee}/{controller=Dashboard}/{action=Index}/{id?}"
//    //defaults: new { area = "Employee" } // Varsayýlan alaný burada belirtin
//);

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var peopleLinkIdentityContext = scope.ServiceProvider.GetRequiredService<PeopleLinkIdentityContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await PeopleLinkIdentityContextSeed.SeedAsync(peopleLinkIdentityContext, roleManager, userManager);
}

app.Run();
