using NAUCountry.ECOMap.Models;

namespace NAUCountry.ECOMap
{
	/// <summary>
	/// Data service responsible for loading all read only tables from a specific CSV file to make accessible for PDF generation.
	/// </summary>
	public class ECODataService
	{
		public IReadOnlyDictionary<int, Commodity> CommodityEntries { get; internal set; }

		public IReadOnlyDictionary<int, County> CountyEntries { get; internal set; }

		public IReadOnlyDictionary<int, Offer> OfferEntries { get; internal set; }
		public IReadOnlyDictionary<int, Practice> PracticeEntries { get; internal set; }

		public IReadOnlyDictionary<Offer, Price> PriceEntries { get; internal set; }

		public IReadOnlyDictionary<string, RecordType> RecordTypeEntries { get; internal set; }

		public IReadOnlyDictionary<int, State> StateEntries { get; internal set; }

		public IReadOnlyDictionary<int, NAUType> TypeEntries { get; internal set; }

	}
}
