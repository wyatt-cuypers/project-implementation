

namespace NAUCountryA.Models
{
    public class Commodity : IEquatable<Commodity>
    {
        public Commodity(int commodityCode, string commodityName, string commodityAbbreviation, char annualPlantingCode, int commodityYear, DateTime releasedDate, string recordTypeCode)
        {
            CommodityCode = commodityCode;
            CommodityName = commodityName;
            CommodityAbbreviation = commodityAbbreviation;
            AnnualPlantingCode = annualPlantingCode;
            CommodityYear = commodityYear;
            ReleasedDate = releasedDate;
            RecordType = Service.RecordTypeEntries[recordTypeCode];
        }

        public Commodity(string line)
        {
            string[] values = line.Split(',');
            CommodityCode = (int)Service.ExpressValue(values[4]);
            CommodityName = (string)Service.ExpressValue(values[5]);
            CommodityAbbreviation = (string)Service.ExpressValue(values[6]);
            AnnualPlantingCode = ((string)Service.ExpressValue(values[7]))[0];
            CommodityYear = (int)Service.ExpressValue(values[3]);
            ReleasedDate = (DateTime)Service.ExpressValue(values[9]);
            RecordType = Service.RecordTypeEntries[(string)Service.ExpressValue(values[0])];
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

        public KeyValuePair<int,Commodity> Pair
        {
            get
            {
                return new KeyValuePair<int,Commodity>(CommodityCode, this);
            }
        }

        public bool Equals(Commodity other)
        {
            return CommodityCode == other.CommodityCode && CommodityName == other.CommodityName &&
            CommodityAbbreviation == other.CommodityAbbreviation && AnnualPlantingCode == other.AnnualPlantingCode &&
            CommodityYear == other.CommodityYear && Service.DateTimeEquals(ReleasedDate, other.ReleasedDate) &&
            RecordType == other.RecordType;
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
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatCommodityCode()},\"{CommodityName}\",\"{CommodityAbbreviation}\",\"" +
                $"{AnnualPlantingCode}\",\"{CommodityYear}\",\"{Service.ToString(ReleasedDate)}\"," +
                $"\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator== (Commodity a, Commodity b)
        {
            return a.Equals(b);
        }

        public static bool operator!= (Commodity a, Commodity b)
        {
            return !a.Equals(b);
        }
    }
}