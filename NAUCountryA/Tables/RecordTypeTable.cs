using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class RecordTypeTable : IReadOnlyDictionary<string, RecordType>
    {
        private readonly IDictionary<string,RecordType> recordTypeEntries;
        public RecordTypeTable()
        {
            recordTypeEntries = new Dictionary<string,RecordType>();
            TrimEntries();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return recordTypeEntries.Count;
            }
        }

        public RecordType this[string recordTypeCode]
        {
            get
            {
                return recordTypeEntries[recordTypeCode];
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return recordTypeEntries.Keys;
            }
        }

        public IEnumerable<RecordType> Values
        {
            get
            {
                return recordTypeEntries.Values;
            }
        }

        public bool ContainsKey(string recordTypeCode)
        {
            return recordTypeEntries.ContainsKey(recordTypeCode);
        }

        public IEnumerator<KeyValuePair<string,RecordType>> GetEnumerator()
        {
            return recordTypeEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(string recordTypeCode, [MaybeNullWhen(false)] out RecordType value)
        {
            return recordTypeEntries.TryGetValue(recordTypeCode, out value);
        }

        private ICollection<ICollection<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A23_Commodity"));
                contents.Add(Service.ToCollection("A23_County"));
                contents.Add(Service.ToCollection("A23_Practice"));
                contents.Add(Service.ToCollection("A23_STATE"));
                contents.Add(Service.ToCollection("A23_Type"));
                return contents;
            }
        }

        private IList<KeyValuePair<string,RecordType>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<string,RecordType>> currentEntries = new List<KeyValuePair<string,RecordType>>();
                foreach (KeyValuePair<string,RecordType> pair in this)
                {
                    currentEntries.Add(pair);
                }
                return currentEntries;
            }
        }

        private void AddEntries()
        {
            ICollection<ICollection<string>> csvContents = CsvContents;
            foreach (ICollection<string> contents in csvContents)
            {
                IEnumerator<string> lines = contents.GetEnumerator();
                if (lines.MoveNext())
                {
                    string headerLine = lines.Current;
                    string[] headers = headerLine.Split(',');
                    while (lines.MoveNext())
                    {
                        string line = lines.Current;
                        string[] values = line.Split(',');
                        string recordTypeCode = (string)Service.ExpressValue(values[0]);
                        int recordCategoryCode = (int)Service.ExpressValue(values[1]);
                        int reinsuranceYear = (int)Service.ExpressValue(values[2]);
                        RecordType recordType = new RecordType(recordTypeCode, recordCategoryCode, reinsuranceYear);
                        if (!recordTypeEntries.ContainsKey(recordTypeCode))
                        {
                            recordTypeEntries.Add(recordType.Pair);
                        }
                    }
                }
            }
        }

        private void TrimEntries()
        {
            ICollection<string> currentCSVContents = new HashSet<string>();
            foreach (ICollection<string> contents in CsvContents)
            {
                foreach(string line in contents)
                {
                    string[] values = line.Split(',');
                    currentCSVContents.Add($"{values[0]},{values[1]},{values[2]}");
                }
            }
            int position = 0;
            while (position < CurrentContents.Count)
            {
                if (!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    recordTypeEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }
        }
    }
}