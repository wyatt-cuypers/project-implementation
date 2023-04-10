using System;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.FileIO;
using NAUCountryA;
using NAUCountryA.Models;
using NAUCountryA.Tables;

Service.LoadTables();
Price testPrice = Service.PriceEntries.First().Value;
Service.GeneratePDF(testPrice.Offer.County.State, testPrice.Offer.Practice.Commodity, 2023);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();