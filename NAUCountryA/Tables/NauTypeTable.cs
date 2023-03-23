using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class NAUTypeTable : IReadOnlyDictionary<int, NAUType>
    {
        // Assigned to Katelyn Runsvold
        private readonly IDictionary<int, NAUType> nautypeEntries;
        public NAUTypeTable()
        {
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

        public bool TryGetValue(int typeCode, [MaybeNullWhen(false)] out NAUType value)
        {
            return nautypeEntries.TryGetValue(typeCode, out value);
        }

        private IEnumerable<string> CsvContents
        {
            get
            {
                return Service.ToCollection("A23_TYPE");
            }
        }
          
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
                    NAUType entry = new NAUType(line);
                    if (!nautypeEntries.ContainsKey(entry.Pair.Key))
                    {
                        nautypeEntries.Add(entry.Pair);
                    }
                }
            }
        }
    }
}
