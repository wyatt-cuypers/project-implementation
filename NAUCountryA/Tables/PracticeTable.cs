using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class PracticeTable : IReadOnlyDictionary<int, Practice>
    {
        // Assigned to Wyatt Cuypers
        private readonly IDictionary<int, Practice> practiceEntries;
        public PracticeTable()
        {
            practiceEntries = new Dictionary<int, Practice>();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return practiceEntries.Count;
            }
        }

        public Practice this[int practiceCode]
        {
            get
            {
                return practiceEntries[practiceCode];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                return practiceEntries.Keys;
            }
        }

        public IEnumerable<Practice> Values
        {
            get
            {
                return practiceEntries.Values;
            }
        }

        public bool ContainsKey(int practiceCode)
        {
            return practiceEntries.ContainsKey(practiceCode);
        }

        public IEnumerator<KeyValuePair<int, Practice>> GetEnumerator()
        {
            return practiceEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int practiceCode, [MaybeNullWhen(false)] out Practice value)
        {
            return practiceEntries.TryGetValue(practiceCode, out value);
        }
        private IEnumerable<string> CsvContents
        {
            get
            {
                return Service.ToCollection("A23_Practice");
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
                    Practice current = new Practice(line);
                    if (!practiceEntries.ContainsKey(current.Pair.Key))
                    {
                        practiceEntries.Add(current.Pair);
                    }
                }
            }
        }
    }
}