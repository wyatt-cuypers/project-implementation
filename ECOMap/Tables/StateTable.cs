using ECOMap.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ECOMap.Tables
{
    public class StateTable : IReadOnlyDictionary<int, State>
    {
        // Assigned to Miranda Ryan
        private readonly IDictionary<int, State> stateEntries;
        private ECODataService ECODataService  { get; }
		private IEnumerable<string> CsvContents { get; }

		public StateTable(ECODataService service, IEnumerable<string> csvContents)
        {
            CsvContents = csvContents;
			ECODataService = service;
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

		// CODE REVIEW: Parsing logic could exist outside DTO
		public bool TryGetValue(int stateCode, [MaybeNullWhen(false)] out State value)
        {
            return stateEntries.TryGetValue(stateCode, out value);
        }

		// CODE REVIEW: Parsing logic could exist outside DTO
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
                    State current = new State(ECODataService, line);
                    if (current.Valid && !stateEntries.ContainsKey(current.Pair.Key))
                    {
                        stateEntries.Add(current.Pair);
                    }
                }
            }
        }
    }
}
