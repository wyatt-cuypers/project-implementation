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
                    State current = new State(line);
                    if (!stateEntries.ContainsKey(current.Pair.Key))
                    {
                        stateEntries.Add(current.Pair);
                    }
                }
            }
        }
    }
}
