using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class CommodityTable : IReadOnlyDictionary<int, Commodity>
    {
        private readonly IDictionary<int,Commodity> commodityEntries;
        public CommodityTable()
        {
            commodityEntries = new Dictionary<int,Commodity>();
            TrimEntries();
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

        public bool TryGetValue(int commodityCode, [MaybeNullWhen(false)] out Commodity value)
        {
            return commodityEntries.TryGetValue(commodityCode, out value);
        }
        private IEnumerable<string> CsvContents
        {
            get
            {
                return Service.ToCollection("A23_Commodity");
            }
        }

        private IList<KeyValuePair<int,Commodity>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<int,Commodity>> currentEntries = new List<KeyValuePair<int,Commodity>>();
                foreach (KeyValuePair<int,Commodity> pair in this)
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
                    Commodity current = new Commodity(lines.Current);
                    if (!commodityEntries.ContainsKey(current.Pair.Key))
                    {
                        commodityEntries.Add(current.Pair);
                    }
                }
            }
        }

        private void TrimEntries()
        {
            ICollection<string> currentCSVContents = new HashSet<string>();
            foreach(string line in CsvContents)
            {
                string[] values = line.Split(',');
                currentCSVContents.Add($"{values[4]},{values[5]},{values[6]},{values[7]},{values[3]},{values[9]},{values[0]}");
            }
            int position = 0;
            while (position < CurrentContents.Count)
            {
                if (!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    commodityEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }
        }
    }
}