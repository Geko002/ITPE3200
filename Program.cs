using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixNote.Models;
using PixNote.Services;  // Add the namespace for DummyEmailSender
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext for Identity and Application Data
builder.Services.AddDbContext<PhotoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PhotoDbContextConnection")));

// Register Identity services with your custom User class
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PhotoDbContext>()
    .AddDefaultTokenProviders(); // Provides default token-based features like password reset, etc.

// Add Razor Pages and configure authorization
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        // Authorize the Manage page or other custom pages
        options.Conventions.AuthorizePage("/Account/Manage");
    });

// Add MVC
builder.Services.AddControllersWithViews();

// Register the DummyEmailSender as the implementation of IEmailSender
builder.Services.AddTransient<IEmailSender, DummyEmailSender>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization middleware
app.UseAuthentication();  
app.UseAuthorization();

// Map Razor Pages for Identity and MVC routes
app.MapRazorPages();  // Ensures Identity pages like /Account/Register are available
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
