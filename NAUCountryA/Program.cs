using NAUCountry.ECOMap;
using NAUCountry.ECOMap.Models;

// Code Review Comment:  This turned into a console application which is perfectly fine but it
// might have been good to create a project for your PDf generation code and then referenced that
// project between the service API you will tie into and the created a console application project (like I did with the map example)
// that does just this (load tables, output result).  Now you have a mix of a console app and API app and it might get very weird
// turning this back into an API.  Keeping things simple and broken out helps with consolidation steps in the future.
//
// Same with unit tests as your unit test project references the NAUCountryA application.  By breaking out the core logic into a nother project
// you can setup unit tests to the core logic and not bring with it the whole application.
//
// I will try to show an example of this.

// DESIGN NOTE / Code Review: having separating out the code into a new project you could easily create a
// console application to test this while reverting this back to the web application needed
try
{
    LoadingProcessor loader = new LoadingProcessor(CsvUtility.InitialPathLocation);
    ECODataService service = await loader.LoadAll();

    Price testPrice = service.PriceEntries.First().Value;
    EcoPdfGenerator.GeneratePDF(service, testPrice.Offer.County.State, testPrice.Offer.Practice.Commodity, 2023);
}
catch (Exception ex)
{
	Console.WriteLine(ex.ToString());
}
//CreatePDF.Run();
//Service.ConstructUser();
// ICollection<string> commodityDataSet = Service.ToCollection("A23_Commodity");
// Console.WriteLine(commodityDataSet.Count);
// ICollection<int> commodityIds = new HashSet<int>();
// IEnumerator<string> commodityEnum = commodityDataSet.GetEnumerator();
// if (commodityEnum.MoveNext())
// {
//     string[] headers = commodityEnum.Current.Split(',');
//     while (commodityEnum.MoveNext())
//     {
//         string[] values = commodityEnum.Current.Split(',');
//         commodityIds.Add(Convert.ToInt32(values[4]));
//     }
//     Console.WriteLine(commodityIds.Count);
// }
// string sqlCommand = $"SELECT * FROM public.\"State\" WHERE \"STATE_CODE\" = 01;";
// System.Data.DataTable table = Service.GetDataTable(sqlCommand);
// Service.GeneratePDF(new State(table.Rows[0]));

Console.WriteLine("PDF Generated.");
// Price testPrice = Service.PriceEntries.Values.First();
// Service.GeneratePDF(testPrice.Offer.State, testPrice.Offer.Practice, testPrice.Offer.Type);


/*var builder = WebApplication.CreateBuilder(args);

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
*/