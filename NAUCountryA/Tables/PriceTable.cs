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
            TrimEntries();
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

        private ICollection<ICollection<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A22_PRICE"));
                contents.Add(Service.ToCollection("A23_Price"));
                return contents;
            }
        }

        private IList<KeyValuePair<Offer, Price>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<Offer, Price>> currentEntries = new List<KeyValuePair<Offer, Price>>();
                foreach (KeyValuePair<Offer,Price> pair in this)
                {
                    currentEntries.Add(pair);
                }
                return currentEntries;
            }

        }

        private void AddEntries()
        {
                IEnumerator<string> lines = Csvcontents.GetEnumerator();
                if (lines.MoveNext())
                {
                    string headerLine = lines.Current;
                    string[] headers = headerLine.Split(',');
                    while (lines.MoveNext())
                    {
                        Price current = new Price(lines.Current);
                        if(!priceEntries.ContainsKey(current.Pair.Key)){
                            priceEntries.Add(current.Pair);
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
                currentCSVContents.Add($"{values[0]},{values[1]}");
    
            }
            int position = 0;
            while(position < CurrentContents.Count)
            {;
                if(!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    priceEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }
        }
    }
}