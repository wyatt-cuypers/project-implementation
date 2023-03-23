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
            TrimEntries();
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

        private IList<KeyValuePair<int, Practice>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<int, Practice>> currentEntries = new List<KeyValuePair<int, Practice>>();
                foreach (KeyValuePair<int, Practice> pair in this)
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
                    Practice current = new Practice(lines.Current);
                    if (!practiceEntries.ContainsKey(current.Pair.Key))
                    {
                        practiceEntries.Add(current.Pair);
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
                currentCSVContents.Add($"{values[4]},{values[5]},{values[6]},{values[3]},{values[8]},{values[0]}");
            }
            int position = 0;
            while (position < CurrentContents.Count)
            {
                if (!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    practiceEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }
        }
    }
}