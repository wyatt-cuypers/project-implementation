


namespace NAUCountry.ECOMap.Models
{
    public class County : IEquatable<County>
    {
        public County(ECODataService service, string line)
        {
            ECODataService = service;

			string[] values = line.Split(',');
            int stateCode = (int)CsvUtility.ExpressValue(values[3]);
            string recordTypeCode = (string)CsvUtility.ExpressValue(values[0]);
            Valid = ValidCounty(stateCode, recordTypeCode);
            if (Valid)
            {
                CountyCode = (int)CsvUtility.ExpressValue(values[4]);
                State = service.StateEntries[stateCode];
                CountyName = (string)CsvUtility.ExpressValue(values[5]);
                RecordType = service.RecordTypeEntries[recordTypeCode];
            }
        }

        private ECODataService ECODataService { get; }

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
            if (!ECODataService.RecordTypeEntries.ContainsKey(recordTypeCode))
            {
                Console.WriteLine($"Record Type Code {recordTypeCode} doesn't exist.");
                return false;
            }
            else if (!ECODataService.StateEntries.ContainsKey(stateCode))
            {
                Console.WriteLine($"State Code {stateCode} doesn't exist");
                return false;
            }
            return true;
        }
    }
}