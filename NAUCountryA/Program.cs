using System;
using ECOMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

LoadingProcessor loader = new LoadingProcessor(EcoGeneralService.InitialPathLocation);
ECODataService service = loader.LoadAll().GetAwaiter().GetResult();

//EcoPdfGenerator.GeneratePDFGroup(service, "Arizona", 2023);
//EcoPdfGenerator.GeneratePDFGroup(service, "Alabama", 2023);
//EcoPdfGenerator.GeneratePDFGroup(service, "New Mexico", 2023);
//EcoPdfGenerator.GeneratePDFGroup(service, "Wisonsin", 2023);
//EcoPdfGenerator.GeneratePDFGroup(service, "Illinois", 2023);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(service.GetType(), service);
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

app.MapFallbackToFile("index.html");

app.Run();
