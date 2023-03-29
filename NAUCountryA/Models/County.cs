


namespace NAUCountryA.Models
{
    public class County : IEquatable<County>
    {
        // Assigned to Wyatt Cuypers
        public County(int countyCode, int stateCode, string countyName, string recordTypeCode)
        {
            CountyCode = countyCode;
            State = Service.StateEntries[stateCode];
            CountyName = countyName;
            RecordType = Service.RecordTypeEntries[recordTypeCode];
        }

        public County(string line)
        {
            string[] values = line.Split(',');
            int stateCode = (int)Service.ExpressValue(values[3]);
            string recordTypeCode = (string)Service.ExpressValue(values[0]);
            Valid = ValidCounty(stateCode, recordTypeCode);
            if (Valid)
            {
                CountyCode = (int)Service.ExpressValue(values[4]);
                State = Service.StateEntries[stateCode];
                CountyName = (string)Service.ExpressValue(values[5]);
                RecordType = Service.RecordTypeEntries[recordTypeCode];
            }
        }

        public int CountyCode
        {
            get;
            private set;
        }

        public State State
        {
            get;
            private set;
        }

        public string CountyName
        {
            get;
            private set;
        }

        public RecordType RecordType
        {
            get;
            private set;
        }

        public KeyValuePair<int,County> Pair
        {
            get
            {
                return new KeyValuePair<int,County>(CountyCode, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
        }

        public bool Equals(County other)
        {
            return CountyCode == other.CountyCode && State == other.State &&
            CountyName == other.CountyName && RecordType == other.RecordType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is County))
            {
                return false;
            }
            return Equals((County)obj);
        }

        public string FormatCountyCode()
        {
            if (CountyCode < 10)
            {
                return $"\"00{CountyCode}\"";
            }
            else if (CountyCode < 100)
            {
                return $"\"0{CountyCode}\"";
            }
            return $"\"{CountyCode}\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatCountyCode()},{State.FormatStateCode()},\"{CountyName}\",\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator== (County a, County b)
        {
            return a.Equals(b);
        }

        public static bool operator!= (County a, County b)
        {
            return !a.Equals(b);
        }

        private bool ValidCounty(int stateCode, string recordTypeCode)
        {
            if (!Service.RecordTypeEntries.ContainsKey(recordTypeCode))
            {
                Console.WriteLine($"Record Type Code {recordTypeCode} doesn't exist.");
                return false;
            }
            else if (!Service.StateEntries.ContainsKey(stateCode))
            {
                Console.WriteLine($"State Code {stateCode} doesn't exist");
                return false;
            }
            return true;
        }
    }
}