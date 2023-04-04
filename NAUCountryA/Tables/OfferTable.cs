using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class OfferTable : IReadOnlyDictionary<int, Offer>
    {
        // Assigned to Miranda Ryan
        private readonly IDictionary<int, Offer> offerEntries;
        public OfferTable()
        {
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

        public bool TryGetValue(int offerID, [MaybeNullWhen(false)] out Offer value)
        {
            return offerEntries.TryGetValue(offerID, out value);
        }

        private IEnumerable<KeyValuePair<int, IEnumerable<string>>> CsvContents
        {
            get
            {
                IDictionary<int, IEnumerable<string>> contents = new Dictionary<int, IEnumerable<string>>();
                contents.Add(2022, Service.ToCollection("A22_INSURANCE_OFFER"));
                contents.Add(2023, Service.ToCollection("A23_INSURANCE_OFFER"));
                return contents;
            }
        }


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
                        Offer current = new Offer($"{line},{pair.Key}");
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