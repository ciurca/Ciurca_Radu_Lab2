﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ciurca_Radu_Lab2.Data;
using Ciurca_Radu_Lab2;
using Ciurca_Radu_Lab2.Hubs;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Ciurca_Radu_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ciurca_Radu_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Ciurca_Radu_Lab2Context' not found.")));

builder.Services.AddSignalR();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/Chat");

app.Run();
