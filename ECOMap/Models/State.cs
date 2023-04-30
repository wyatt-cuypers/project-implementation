

namespace ECOMap.Models
{
    public class State : IEquatable<State>
    {

        // Assigned to Miranda Ryan
        public State(int stateCode, string stateName, string stateAbbreviation, string recordTypeCode, RecordType recordType)
        {
            StateCode = stateCode;
            StateName = stateName;
            StateAbbreviation = stateAbbreviation;
            RecordType = recordType; // Service.RecordTypeEntries[recordTypeCode];
        }

        public State(ECODataService service, string line)
        {
            string[] values = line.Split(',');
            string recordTypeCode = CsvUtility.ParseAsString(values[0]);
            Valid = service.RecordTypeEntries.ContainsKey(recordTypeCode);
            if (Valid)
            {
                StateCode = CsvUtility.ParseAsInt(values[3]);
                StateName = CsvUtility.ParseAsString(values[4]);
                StateAbbreviation = CsvUtility.ParseAsString(values[5]);
                RecordType = service.RecordTypeEntries[recordTypeCode];
            }
            else
            {
                Console.WriteLine($"Record Type Code {recordTypeCode} doesn't exist.");
            }
        }

        public int StateCode
        {
            get;
            private set;
        }

        public string StateName
        {
            get;
            private set;
        }

        public string StateAbbreviation
        {
            get;
            private set;
        }
        public RecordType RecordType
        {
            get;
            private set;
        }

        public KeyValuePair<int, State> Pair
        {
            get
            {
                return new KeyValuePair<int, State>(StateCode, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
        }

        public bool Equals(State other)
        {
            return StateCode == other.StateCode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is State))
            {
                return false;
            }
            return Equals((State)obj);
        }

        public string FormatStateCode()
        {
            if (StateCode < 10)
            {
                return $"\"0{StateCode}\"";
            }
            return $"\"{StateCode}\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatStateCode()},\"{StateName}\",\"{StateAbbreviation}\",\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator ==(State a, State b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(State a, State b)
        {
            return !a.Equals(b);
        }
    }
}