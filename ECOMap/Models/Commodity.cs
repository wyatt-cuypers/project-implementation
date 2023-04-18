

namespace ECOMap.Models
{
    public class Commodity : IEquatable<Commodity>
    {
        public Commodity(int commodityCode, string commodityName, string commodityAbbreviation, char annualPlantingCode, int commodityYear, DateTime releasedDate, string recordTypeCode, RecordType recordType)
        {
            CommodityCode = commodityCode;
            CommodityName = commodityName;
            CommodityAbbreviation = commodityAbbreviation;
            AnnualPlantingCode = annualPlantingCode;
            CommodityYear = commodityYear;
            ReleasedDate = releasedDate;
            RecordType = recordType; // Service.RecordTypeEntries[recordTypeCode];
        }

        public Commodity(ECODataService service, string line)
        {
            string[] values = line.Split(',');
            string recordTypeCode = (string)CsvUtility.ExpressValue(values[0]);
            Valid = service.RecordTypeEntries.ContainsKey(recordTypeCode);
            if (Valid)
            {
                CommodityCode = (int)CsvUtility.ExpressValue(values[4]);
                CommodityName = (string)CsvUtility.ExpressValue(values[5]);
                CommodityAbbreviation = (string)CsvUtility.ExpressValue(values[6]);
                AnnualPlantingCode = ((string)CsvUtility.ExpressValue(values[7]))[0];
                CommodityYear = (int)CsvUtility.ExpressValue(values[3]);
                ReleasedDate = (DateTime)CsvUtility.ExpressValue(values[9]);
                RecordType = service.RecordTypeEntries[recordTypeCode];
            }
            else
            {
                Console.WriteLine($"Record Type Code {recordTypeCode} doesn't exist");
            }
        }

        public int CommodityCode
        {
            get;
            private set;
        }

        public string CommodityName
        {
            get;
            private set;
        }

        public string CommodityAbbreviation
        {
            get;
            private set;
        }

        public char AnnualPlantingCode
        {
            get;
            private set;
        }

        public int CommodityYear
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

        public KeyValuePair<int, Commodity> Pair
        {
            get
            {
                return new KeyValuePair<int, Commodity>(CommodityCode, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
        }

        public bool Equals(Commodity other)
        {
            return CommodityCode == other.CommodityCode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Commodity))
            {
                return false;
            }
            return Equals((Commodity)obj);
        }

        public string FormatCommodityCode()
        {
            if (CommodityCode < 10)
            {
                return $"\"000{CommodityCode}\"";
            }
            else if (CommodityCode < 100)
            {
                return $"\"00{CommodityCode}\"";
            }
            else if (CommodityCode < 1000)
            {
                return $"\"0{CommodityCode}\"";
            }
            return $"\"{CommodityCode}\"";
        }

        public override int GetHashCode()
        {
            return this.CommodityCode.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatCommodityCode()},\"{CommodityName}\",\"{CommodityAbbreviation}\",\"" +
                $"{AnnualPlantingCode}\",\"{CommodityYear}\",\"{CsvUtility.ToString(ReleasedDate)}\"," +
                $"\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator ==(Commodity a, Commodity b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Commodity a, Commodity b)
        {
            return !a.Equals(b);
        }
    }
}