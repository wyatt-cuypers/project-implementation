using NAUCountry.ECOMap.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountry.ECOMap.Tables
{
    public class CommodityTable : IReadOnlyDictionary<int, Commodity>
    {
        private readonly IDictionary<int,Commodity> commodityEntries;
        private ECODataService ECODataService { get; }
		private IEnumerable<string> CsvContents { get; }

		public CommodityTable(ECODataService service, IEnumerable<string> csvContents)
        {
			ECODataService = service;
            CsvContents = csvContents;

			commodityEntries = new Dictionary<int,Commodity>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return commodityEntries.Count;
            }
        }

        public Commodity this[int commodityCode]
        {
            get
            {
                return commodityEntries[commodityCode];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                return commodityEntries.Keys;
            }
        }

        public IEnumerable<Commodity> Values
        {
            get
            {
                return commodityEntries.Values;
            }
        }

        public bool ContainsKey(int commodityCode)
        {
            return commodityEntries.ContainsKey(commodityCode);
        }

        public IEnumerator<KeyValuePair<int,Commodity>> GetEnumerator()
        {
            return commodityEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
		public bool TryGetValue(int commodityCode, [MaybeNullWhen(false)] out Commodity value)
        {
            return commodityEntries.TryGetValue(commodityCode, out value);
        }

        // CODE REVIEW: Parsing logic could exist outside DTO
        private void AddEntries()
        {
            bool isHeader = true;
            foreach (string line in CsvContents)
            {
                if (isHeader)
                {
                    isHeader = !isHeader;
                }
                else
                {
                    Commodity current = new Commodity(ECODataService, line);
                    if (current.Valid && !commodityEntries.ContainsKey(current.Pair.Key))
                    {
                        commodityEntries.Add(current.Pair);
                    }
                }
            }
        }
    }
}