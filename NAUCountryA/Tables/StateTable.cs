using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class StateTable : IReadOnlyDictionary<int, State>
    {
        // Assigned to Miranda Ryan
        private readonly IDictionary<int, State> stateEntries;
        public StateTable()
        {
            stateEntries = new Dictionary<int, State>();
            TrimEntries();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return stateEntries.Count;
            }
        }

        public State this[int stateCode]
        {
            get
            {
                return stateEntries[stateCode];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                return stateEntries.Keys;
            }
        }

        public IEnumerable<State> Values
        {
            get
            {
                return stateEntries.Values;
            }
        }

        public bool ContainsKey(int stateCode)
        {
            return stateEntries.ContainsKey(stateCode);
        }

        public IEnumerator<KeyValuePair<int, State>> GetEnumerator()
        {
            return stateEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int stateCode, [MaybeNullWhen(false)] out State value)
        {
            return stateEntries.TryGetValue(stateCode, out value);
        }

        private IEnumerable<string> CsvContents
        {
            get
            {
                return Service.ToCollection("A23_STATE");
            }
        }

        private IList<KeyValuePair<int, State>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<int, State>> currentEntries = new List<KeyValuePair<int, State>>();
                foreach (KeyValuePair<int, State> pair in this)
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
                    State current = new State(lines.Current);
                    if (!stateEntries.ContainsKey(current.Pair.Key))
                    {
                        stateEntries.Add(current.Pair);
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
                currentCSVContents.Add($"{values[3]},{values[4]},{values[5]},{values[0]}");
            }
            int position = 0;
            while (position < CurrentContents.Count)
            {
                if (!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    stateEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }
        }
       
    }
}
