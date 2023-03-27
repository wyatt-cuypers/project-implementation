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
                    Commodity current = new Commodity(line);
                    if (current.Valid && !commodityEntries.ContainsKey(current.Pair.Key))
                    {
                        commodityEntries.Add(current.Pair);
                    }
                }
            }
        }
    }
}