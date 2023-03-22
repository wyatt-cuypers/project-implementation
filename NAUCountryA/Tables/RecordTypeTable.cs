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

        private IEnumerable<IEnumerable<string>> CsvContents
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
                        RecordType recordType = new RecordType(line);
                        if (!recordTypeEntries.ContainsKey(recordType.Pair.Key))
                        {
                            recordTypeEntries.Add(recordType.Pair);
                        }
                    }
                }
            }
        }
    }
}