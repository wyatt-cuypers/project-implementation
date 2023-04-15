using System;
using NAUCountry.ECOMap;
using NAUCountry.ECOMap.Models;

// LoadingProcessor loader = new LoadingProcessor(CsvUtility.InitialPathLocation);
// ECODataService service = await loader.LoadAll();
// string stateName = "Minnesota";
// State hereState = null;
// int year = 2023;
// Parallel.ForEach(service.StateEntries, state =>
// {
//     if(state.Value.StateName.Equals(stateName)) {
//         hereState = new State(state.Value.StateCode, state.Value.StateName, state.Value.StateAbbreviation, state.Value.RecordType.RecordTypeCode, state.Value.RecordType);
//     }
// });
// NAUCountry.ECOMap.EcoPdfGenerator.GeneratePDFGroup(service, hereState, year);
//if(state != null) {
    //Console.WriteLine(state.StateName);
//}
/*Price testPrice = service.PriceEntries.First().Value;
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