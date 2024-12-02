using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ciurca_Radu_Lab2.Data;
using Ciurca_Radu_Lab2;
using Ciurca_Radu_Lab2.Hubs;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Ciurca_Radu_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ciurca_Radu_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Ciurca_Radu_Lab2Context' not found.")));

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ciurca_Radu_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Ciurca_Radu_Lab2Context' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

});

builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("OnlySales", policy => {
        policy.RequireClaim("Department", "Sales");
    });
});

builder.Services.AddSignalR();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("SalesManager", policy => {
        policy.RequireRole("Manager");
        policy.RequireClaim("Department", "Sales");
    });
});
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/Chat");

app.MapRazorPages();
app.Run();
