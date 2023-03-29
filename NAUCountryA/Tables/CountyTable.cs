using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;


namespace NAUCountryA.Tables
{
    public class CountyTable : IReadOnlyDictionary<int, County>
    {
        // Assigned to Wyatt Cuypers
        private readonly IDictionary<int, County> countyEntries;
        public CountyTable()
        {
            countyEntries = new Dictionary<int, County>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return countyEntries.Count;
            }
        }

        public County this[int countyCode]
        {
            get
            {
                return countyEntries[countyCode];
            }
        }
        public IEnumerable<int> Keys
        {
            get
            {
                return countyEntries.Keys;
            }
        }

        public IEnumerable<County> Values
        {
            get
            {
                return countyEntries.Values;
            }
        }

        public bool ContainsKey(int countyCode)
        {
            return countyEntries.ContainsKey(countyCode);
        }

        public IEnumerator<KeyValuePair<int, County>> GetEnumerator()
        {
            return countyEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int countyCode, [MaybeNullWhen(false)] out County value)
        {
            return countyEntries.TryGetValue(countyCode, out value);
        }
        private IEnumerable<string> CsvContents
        {
            get
            {
                return Service.ToCollection("A23_County");
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
                    County current = new County(line);
                    if (current.Valid && !countyEntries.ContainsKey(current.Pair.Key))
                    {
                        countyEntries.Add(current.Pair);
                    }
                }
            }
        }
    }
}