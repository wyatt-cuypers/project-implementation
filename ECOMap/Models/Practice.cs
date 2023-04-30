

namespace ECOMap.Models
{
    public class Practice : IEquatable<Practice>
    {
        public Practice(ECODataService service, string line)
        {
            ECODataService = service;

			string[] values = line.Split(',');
            string recordTypeCode = CsvUtility.ParseAsString(values[0]);
            int commodityCode = CsvUtility.ParseAsInt(values[3]);
            Valid = ValidPractice(recordTypeCode, commodityCode);
            if (Valid)
            {
                PracticeCode = CsvUtility.ParseAsInt(values[4]);
                PracticeName = CsvUtility.ParseAsString(values[5]);
                PracticeAbbreviation = CsvUtility.ParseAsString(values[6]);
                Commodity = service.CommodityEntries[commodityCode];
                ReleasedDate = CsvUtility.ParseAsDateTime(values[8]);
                RecordType = service.RecordTypeEntries[recordTypeCode];
            }
            
        }

        private ECODataService ECODataService { get; }

		public int PracticeCode
        {
            get;
            private set;
        }

        public string PracticeName
        {
            get;
            private set;
        }

        public string PracticeAbbreviation
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

        public KeyValuePair<int, Practice> Pair
        {
            get
            {
                return new KeyValuePair<int, Practice>(PracticeCode, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
        }

        public bool Equals(Practice other)
        {
            return PracticeCode == other.PracticeCode && PracticeName == other.PracticeName &&
            PracticeAbbreviation == other.PracticeAbbreviation && Commodity == other.Commodity &&
			CsvUtility.DateTimeEquals(ReleasedDate, other.ReleasedDate) && RecordType == other.RecordType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Practice))
            {
                return false;
            }
            return Equals((Practice)obj);
        }

        public string FormatPracticeCode()
        {
            if (PracticeCode < 10)
            {
                return $"\"00{PracticeCode}\"";
            }
            else if (PracticeCode < 100)
            {
                return $"\"0{PracticeCode}\"";
            }
            return $"\"{PracticeCode}\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatPracticeCode()},\"{PracticeName}\",\"{PracticeAbbreviation}\"," +
                $"{Commodity.FormatCommodityCode()},\"{CsvUtility.ToString(ReleasedDate)}\",\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator ==(Practice a, Practice b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Practice a, Practice b)
        {
            return !a.Equals(b);
        }

        private bool ValidPractice(string recordTypeCode, int commodityCode)
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