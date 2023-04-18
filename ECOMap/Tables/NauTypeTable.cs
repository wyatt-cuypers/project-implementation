using ECOMap.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ECOMap.Tables
{
    public class NAUTypeTable : IReadOnlyDictionary<int, NAUType>
    {
        // Assigned to Katelyn Runsvold
        private readonly IDictionary<int, NAUType> nautypeEntries;
        private ECODataService ECODataService { get; }
		private IEnumerable<string> CsvContents { get; }

		public NAUTypeTable(ECODataService service, IEnumerable<string> csvContents)
        {
            CsvContents = csvContents;
			ECODataService = service;

			nautypeEntries = new Dictionary<int, NAUType>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return nautypeEntries.Count;
            }
        }

        public NAUType this[int typeCode]
        {
            get
            {
                return nautypeEntries[typeCode];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                return nautypeEntries.Keys;
            }
        }

        public IEnumerable<NAUType> Values
        {
            get
            {
                return nautypeEntries.Values;
            }
        }

        public bool ContainsKey(int typeCode)
        {
            return nautypeEntries.ContainsKey(typeCode);
        }

        public IEnumerator<KeyValuePair<int, NAUType>> GetEnumerator()
        {
            return nautypeEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
		public bool TryGetValue(int typeCode, [MaybeNullWhen(false)] out NAUType value)
        {
            return nautypeEntries.TryGetValue(typeCode, out value);
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
                    NAUType entry = new NAUType(ECODataService, line);
                    if (entry.Valid && !nautypeEntries.ContainsKey(entry.Pair.Key))
                    {
                        nautypeEntries.Add(entry.Pair);
                    }
                }
            }
        }
    }
}
