using NAUCountryA.Tables;
using System.Data;

namespace NAUCountryA.Models
{
    public class Commodity : IEquatable<Commodity>
    {
        public Commodity(int commodityCode, string commodityName, string commodityAbbreviation, string annualPlantingCode, int commodityYear, DateTime releasedDate, string recordTypeCode)
        {
            CommodityCode = commodityCode;
            CommodityName = commodityName;
            CommodityAbbreviation = commodityAbbreviation;
            AnnualPlantingCode = annualPlantingCode;
            CommodityYear = commodityYear;
            ReleasedDate = releasedDate;
            IReadOnlyDictionary<string,RecordType> recordTypeEntries = new RecordTypeTable();
            RecordType = recordTypeEntries[recordTypeCode];
        }

        public Commodity(DataRow row)
        :this((int)row["COMMODITY_CODE"], (string)row["COMMODITY_NAME"], (string)row["COMMODITY_ABBREVIATION"],(string)row["ANNUAL_PLANTING_CODE"], (int)row["COMMODITY_YEAR"], (DateTime)row["RELEASED_DATE"], (string)row["RECORD_TYPE_CODE"])
        {
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

        public string AnnualPlantingCode
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

        public bool Equals(Commodity other)
        {
            return CommodityCode == other.CommodityCode && CommodityName == other.CommodityName &&
            CommodityAbbreviation == other.CommodityAbbreviation && AnnualPlantingCode == other.AnnualPlantingCode &&
            CommodityYear == other.CommodityYear && Service.DateTimeEquals(ReleasedDate, other.ReleasedDate) &&
            RecordType == other.RecordType;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
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