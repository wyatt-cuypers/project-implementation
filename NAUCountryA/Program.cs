using System;
using NAUCountry.ECOMap;
using NAUCountry.ECOMap.Models;

/*LoadingProcessor loader = new LoadingProcessor(CsvUtility.InitialPathLocation);
ECODataService service = await loader.LoadAll();
// for(int i = 0; i < service.CommodityEntries.Count; i++) {
//     Console.WriteLine(service.CommodityEntries.ElementAt(i).Value.CommodityName);
// }
Price testPrice = service.PriceEntries.First().Value;
//EcoPdfGenerator.GeneratePDF(service, testPrice.Offer.County.State, testPrice.Offer.Practice.Commodity, 2023);

EcoPdfGenerator.GenerateAllPDFs(service, 2023);
Console.WriteLine("PDFs Generated.");*/


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

app.MapFallbackToFile("index.html"); ;

app.Run();