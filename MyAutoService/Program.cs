using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Utility;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<CookiePolicyOptions>(option =>
{
    option.CheckConsentNeeded = context => true;
    option.MinimumSameSitePolicy = SameSiteMode.None;
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AutoConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/// کد های مربوط به Identity
#region Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddTransient<IEmailSender, EmailSender>();
/// <summary>
/// کد های زیر برای تغییر مسیر پیشفرض قسمت های سیستم
/// </summary>
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
/// <summary>
/// دستور مربوط به احراز هویت کاربر
/// </summary>
app.UseAuthentication();
/// <summary>
/// /دستور مربوط به سطح دسترسی کاربر
/// </summary>
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
