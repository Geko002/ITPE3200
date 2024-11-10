using Microsoft.EntityFrameworkCore;
using PixNote.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<PhotoDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:PhotoDbContextConnection"]);
        
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PhotoDbContext>();
    dbContext.Database.EnsureCreated();
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();


app.MapDefaultControllerRoute();

 //app.MapControllerRoute(
   //  name: "default",
    // pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

