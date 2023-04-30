

namespace ECOMap.Models
{
    public class NAUType : IEquatable<NAUType>
    {
        public NAUType(ECODataService service, string line)
        {
            ECODataService = service;

			string[] values = line.Split(',');
            string recordTypeCode = CsvUtility.ParseAsString(values[0]);
            int commodityCode = CsvUtility.ParseAsInt(values[3]);
            Valid = ValidType(recordTypeCode, commodityCode);
            if (Valid)
            {
                TypeCode = CsvUtility.ParseAsInt(values[4]);
                TypeName = CsvUtility.ParseAsString(values[5]);
                TypeAbbreviation = CsvUtility.ParseAsString(values[6]);
                Commodity = service.CommodityEntries[commodityCode];
                ReleasedDate = CsvUtility.ParseAsDateTime(values[8]);
                RecordType = service.RecordTypeEntries[recordTypeCode];
            }
        }

        private ECODataService ECODataService { get; }

		public int TypeCode
        {
            get;
            private set;
        }

        public string TypeName
        {
            get;
            private set;
        }

        public string TypeAbbreviation
        {
            get;
            private set;
        }

        public Commodity Commodity
        {
            get;
            private set;
        }

        public DateTime ReleasedDate
        {
            get;
            private set;
        }

        public RecordType RecordType
        {
            get;
            private set;
        }

        public KeyValuePair<int, NAUType> Pair
        {
            get
            {
                return new KeyValuePair<int, NAUType>(TypeCode, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
        }

        public bool Equals(NAUType other)
        {
            return TypeCode == other.TypeCode &&
                TypeName == other.TypeName &&
                TypeAbbreviation == other.TypeAbbreviation &&
                Commodity == other.Commodity &&
                CsvUtility.DateTimeEquals(ReleasedDate, other.ReleasedDate) &&
                RecordType == other.RecordType;
        }

        public string FormatTypeCode()
        {
            if (TypeCode < 10)
            {
                return $"\"00{TypeCode}\"";
            }
            else if (TypeCode < 100)
            {
                return $"\"0{TypeCode}\"";
            }
            return $"\"{TypeCode}\"";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NAUType))
            {
                return false;
            }
            return Equals((NAUType)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatTypeCode()},\"{TypeName}\",\"{TypeAbbreviation}\",{Commodity.FormatCommodityCode()}" +
                $"\"{CsvUtility.ToString(ReleasedDate)}\",\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator ==(NAUType a, NAUType b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(NAUType a, NAUType b)
        {
            return !a.Equals(b);
        }

        private bool ValidType(string recordTypeCode, int commodityCode)
        {
            if (!ECODataService.RecordTypeEntries.ContainsKey(recordTypeCode))
            {
                Console.WriteLine($"Record Type Code {recordTypeCode} doesn't exist.");
                return false;
            }
            else if (!ECODataService.CommodityEntries.ContainsKey(commodityCode))
            {
                Console.WriteLine($"Commodity Code {commodityCode} doesn't exist.");
                return false;
            }
            return true;
        }
    }
}