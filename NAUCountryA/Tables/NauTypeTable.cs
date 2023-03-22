using NAUCountryA.Models;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class NAUTypeTable : IReadOnlyDictionary<int, NAUType>
    {
        // Assigned to Katelyn Runsvold
        private readonly IDictionary<int, NAUType> nautypeEntries;
        public NAUTypeTable()
        {
            nautypeEntries = new Dictionary<int, NAUType>();
            TrimEntries();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return nautypeEntries.Count;
            }
        }

        public NAUType this[int typeCode]
        {
            get
            {
                return nautypeEntries[typeCode];
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                return nautypeEntries.Keys;
            }
        }

        public IEnumerable<NAUType> Values
        {
            get
            {
                return nautypeEntries.Values;
            }
        }

        public bool ContainsKey(int typeCode)
        {
            return nautypeEntries.ContainsKey(typeCode);
        }

        public IEnumerator<KeyValuePair<int, NAUType>> GetEnumerator()
        {
            return nautypeEntries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int typeCode, [MaybeNullWhen(false)] out NAUType value)
        {
            return nautypeEntries.TryGetValue(typeCode, out value);
        }

        private IEnumerable<string> CsvContents
        {
            get
            {
                return Service.ToCollection("A23_TYPE");
            }
        }

        private IList<KeyValuePair<int,NAUType>> CurrentContents
        {
            get
            {
                IList<KeyValuePair<int,NAUType>> currentEntries = new List<KeyValuePair<int,Commodity>>();
                foreach (KeyValuePair<int,NAUType> pair in this)
                {
                    currentEntries.Add(pair);
                }
                return currentEntries;
            }
        }
          

        private void AddEntries()
        {
            IEnumerator<string> lines = CsvContents.GetEnumerator();
            if(lines.MoveNext())
            {
                string headerLine = lines.Current;
                string[] headers = headerLine.Split(',');
                while (lines.MoveNext())
                {
                    string line = lines.Current;
                    string[] values = line.Split(",");
                    int typeCode = (int)Service.ExpressValue(values[4]);
                    string typeName = (string)Service.ExpressValue(values[5]);
                    string typeAbbreviation = (string)Service.ExpressValue(values[6]);
                    int commodityCode = (int)Service.ExpressValue(values[3]); 
                    DateTime releasedDate = (DateTime)Service.ExpressValue(values[8]);
                    string recordTypeCode = (string)Service.ExpressValue(values[0]);
                    if(!nautypeEntries.ContainsKey(typeCode))
                    {
                        nautypeEntries.Add(typeCode.Pair);
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
                if(!currentCSVContents.Contains(CurrentContents[position].Value.ToString()))
                {
                    nautypeEntries.Remove(CurrentContents[position]);
                }
                else
                {
                    position++;
                }
            }

        }

    }
}
