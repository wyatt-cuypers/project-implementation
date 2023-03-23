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
            TrimEntries();
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

        private IList<KeyValuePair<int, County>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<int, County>> currentEntries = new List<KeyValuePair<int, County>>();
                foreach (KeyValuePair<int, County> pair in this)
                {
                    currentEntries.Add(pair);
                }
                return currentEntries;
            }
        }

        private void AddEntries()
        {
            IEnumerator<string> lines = CsvContents.GetEnumerator();
            if (lines.MoveNext())
            {
                string headerLine = lines.Current;
                string[] headers = headerLine.Split(',');
                while (lines.MoveNext())
                {
                    County current = new County(lines.Current);
                    if (!countyEntries.ContainsKey(current.Pair.Key))
                    {
                        countyEntries.Add(current.Pair);
                    }
                }
            }
        }

        private void TrimEntries()
        {
            ICollection<string> currentCSVContents = new HashSet<string>();
            foreach (string line in CsvContents)
            {
                string[] values = line.Split(',');
                currentCSVContents.Add($"{values[4]},{values[3]},{values[5]},{values[0]}");
            }
            int position = 0;
            while (position < CurrentContents.Count)
            {
                if (!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    countyEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }
        }
    }
}