
using Microsoft.VisualBasic.FileIO;
using NAUCountry.ECOMap.Tables;

namespace NAUCountry.ECOMap
{
	public class LoadingProcessor
	{
		public string InitialPathLocation { get; }

		public LoadingProcessor(string initialPath) 
		{
			InitialPathLocation = initialPath;
		}

		public async Task<ECODataService> LoadAll()
		{
			var a23Commodity = LoadLinesFromCsv("A23_Commodity");
			var a23County = LoadLinesFromCsv("A23_County");
			var a23Practice = LoadLinesFromCsv("A23_Practice");
			var a23State = LoadLinesFromCsv("A23_STATE");
			var a23type = LoadLinesFromCsv("A23_Type");
			var a22InsuranceOffer = LoadLinesFromCsv("A22_INSURANCE_OFFER");
			var a23InsuranceOffer = LoadLinesFromCsv("A23_INSURANCE_OFFER");
			var a22Price = LoadLinesFromCsv("A22_PRICE");
			var a23Price = LoadLinesFromCsv("A23_Price");

			ECODataService service = new ECODataService();
			service.RecordTypeEntries = new RecordTypeTable(new List<ICollection<string>>
			{
				await a23Commodity,
				await a23County,
				await a23Practice,
				await a23State,
				await a23type
			});

			service.CommodityEntries = new CommodityTable(service, await a23Commodity);
			service.StateEntries = new StateTable(service, await a23State);
			service.CountyEntries = new CountyTable(service, await a23County);
			service.TypeEntries = new NAUTypeTable(service, await a23type);
			service.PracticeEntries = new PracticeTable(service, await a23Practice);

			service.OfferEntries = new OfferTable(service, new Dictionary<int, IEnumerable<string>>
			{
				{ 2022, await a22InsuranceOffer },
				{ 2023, await a23InsuranceOffer }
			});

			service.PriceEntries = new PriceTable(service, new List<ICollection<string>>
			{
				await a22Price,
				await a23Price 
			});

			Console.WriteLine("All data loaded");

			return service;
		}

		public async Task<ECODataService> LoadPriceAndRecordType()
		{
			var a23Commodity = LoadLinesFromCsv("A23_Commodity");
			var a23County = LoadLinesFromCsv("A23_County");
			var a23Practice = LoadLinesFromCsv("A23_Practice");
			var a23State = LoadLinesFromCsv("A23_STATE");
			var a23type = LoadLinesFromCsv("A23_Type");
			var a22Price = LoadLinesFromCsv("A22_PRICE");
			var a23Price = LoadLinesFromCsv("A23_Price");

			ECODataService service = new ECODataService();
			service.RecordTypeEntries = new RecordTypeTable(new List<ICollection<string>>
			{
				await a23Commodity,
				await a23County,
				await a23Practice,
				await a23State,
				await a23type
			});
			service.StateEntries = new StateTable(service, await a23State);
			service.PriceEntries = new PriceTable(service, new List<ICollection<string>>
			{
				await a22Price,
				await a23Price 
			});

			Console.WriteLine("All price and offer data loaded");

			return service;
		}

		public async Task<ECODataService> LoadCommodities()
		{
			var a23Commodity = LoadLinesFromCsv("A23_Commodity");

			ECODataService service = new ECODataService();
			service.RecordTypeEntries = new RecordTypeTable(new List<ICollection<string>>
			{
				await a23Commodity,
			});

			service.CommodityEntries = new CommodityTable(service, await a23Commodity);

			Console.WriteLine("All commodities loaded");

			return service;
		}

		public async Task<ICollection<string>> LoadLinesFromCsv(string csvFileName)
		{
			return await Task.Run(() =>
			{
				var startTime = DateTime.Now;
				Console.WriteLine($"{csvFileName} started loading at {startTime}");

				ICollection<string> lines = new List<string>();

				string filePath = Path.Combine(InitialPathLocation, "NAUCountryA", "Resources", csvFileName + ".csv");
				Console.WriteLine($"Loading {filePath}");

				TextFieldParser csvParcer = new TextFieldParser(filePath);
				csvParcer.TextFieldType = FieldType.Delimited;
				while (!csvParcer.EndOfData)
				{
					lines.Add(csvParcer.ReadLine());
				}
				csvParcer.Close();

				Console.WriteLine($"{csvFileName} loaded and took {DateTime.Now.Subtract(startTime):hh\\:mm\\:ss} to complete");

				return lines;
			});
		}
	}
}
