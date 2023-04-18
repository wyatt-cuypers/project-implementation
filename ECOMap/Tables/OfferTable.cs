using ECOMap.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ECOMap.Tables
{
    public class OfferTable : IReadOnlyDictionary<int, Offer>
    {
        // Assigned to Miranda Ryan
        private readonly IDictionary<int, Offer> offerEntries;
        private ECODataService ECODataService { get; }
		private IEnumerable<KeyValuePair<int, IEnumerable<string>>> CsvContents { get; }

		public OfferTable(ECODataService service, IEnumerable<KeyValuePair<int, IEnumerable<string>>> csvContents)
        {
            CsvContents = csvContents;
			ECODataService = service;

			offerEntries = new Dictionary<int, Offer>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return offerEntries.Count;
            }
        }

        public Offer this[int offerID]
        {
            get
            {
                return offerEntries[offerID];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                return offerEntries.Keys;
            }
        }

        public IEnumerable<Offer> Values
        {
            get
            {
                return offerEntries.Values;
            }
        }

        public bool ContainsKey(int offerID)
        {
            return offerEntries.ContainsKey(offerID);
        }

        public IEnumerator<KeyValuePair<int, Offer>> GetEnumerator()
        {
            return offerEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
		public bool TryGetValue(int offerID, [MaybeNullWhen(false)] out Offer value)
        {
            return offerEntries.TryGetValue(offerID, out value);
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
		private void AddEntries()
        {
            foreach (KeyValuePair<int, IEnumerable<string>> pair in CsvContents)
            {
                bool isHeader = true;
                foreach (string line in pair.Value)
                {
                    if (isHeader)
                    {
                        isHeader = !isHeader;
                    }
                    else
                    {
                        Offer current = new Offer(ECODataService, $"{line},{pair.Key}");
                        if (current.Valid && !offerEntries.ContainsKey(current.Pair.Key))
                        {
                            offerEntries.Add(current.Pair);
                        }
                    }
                }
            }
        }
    }
}