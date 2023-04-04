using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class PriceTable : IReadOnlyDictionary<Offer, Price>
    {
        private readonly IDictionary<Offer, Price> priceEntries;
        // Assigned to Katelyn Runsvold
        public PriceTable()
        {
            priceEntries = new Dictionary<Offer, Price>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return priceEntries.Count;
            }
        }

        public Price this[Offer offer]
        {
            get
            {
                return priceEntries[offer];
            }
        }

        public IEnumerable<Offer> Keys
        {
            get
            {
                return priceEntries.Keys;
            }
        }

        public IEnumerable<Price> Values
        {
            get
            {
                return priceEntries.Values;
            }
        }

        public bool ContainsKey(Offer offer)
        {
            return priceEntries.ContainsKey(offer);
        }

        public IEnumerator<KeyValuePair<Offer, Price>> GetEnumerator()
        {
            return priceEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(Offer offer, [MaybeNullWhen(false)] out Price value)
        {
            return priceEntries.TryGetValue(offer, out value);
        }

        private IEnumerable<IEnumerable<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A22_PRICE"));
                contents.Add(Service.ToCollection("A23_Price"));
                return contents;
            }
        }

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
                        Price current = new Price(line);
                        if (current.Valid && !priceEntries.ContainsKey(current.Pair.Key))
                        {
                            priceEntries.Add(current.Pair);
                        }
                    }
                }
            }
        }
    }
}