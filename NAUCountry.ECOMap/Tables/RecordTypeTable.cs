using NAUCountry.ECOMap.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountry.ECOMap.Tables
{
    public class RecordTypeTable : IReadOnlyDictionary<string, RecordType>
    {
        private readonly IDictionary<string,RecordType> recordTypeEntries;
		private IEnumerable<IEnumerable<string>> CsvContents { get; }

		public RecordTypeTable(IEnumerable<IEnumerable<string>> csvContents)
        {
            CsvContents = csvContents;
			recordTypeEntries = new Dictionary<string,RecordType>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return recordTypeEntries.Count;
            }
        }

        public RecordType this[string recordTypeCode]
        {
            get
            {
                return recordTypeEntries[recordTypeCode];
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return recordTypeEntries.Keys;
            }
        }

        public IEnumerable<RecordType> Values
        {
            get
            {
                return recordTypeEntries.Values;
            }
        }

        public bool ContainsKey(string recordTypeCode)
        {
            return recordTypeEntries.ContainsKey(recordTypeCode);
        }

        public IEnumerator<KeyValuePair<string,RecordType>> GetEnumerator()
        {
            return recordTypeEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
		public bool TryGetValue(string recordTypeCode, [MaybeNullWhen(false)] out RecordType value)
        {
            return recordTypeEntries.TryGetValue(recordTypeCode, out value);
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
		private void AddEntries()
        {
            foreach (IEnumerable<string> contents in CsvContents)
            {
                bool isHeader = true;
                foreach (string line in contents)
                {
                    if (isHeader)
                    {
                        isHeader = !isHeader;
                    }
                    else
                    {
                        RecordType recordType = new RecordType(line);
                        if (!recordTypeEntries.ContainsKey(recordType.Pair.Key))
                        {
                            recordTypeEntries.Add(recordType.Pair);
                        }
                    }
                }
            }
        }
    }
}