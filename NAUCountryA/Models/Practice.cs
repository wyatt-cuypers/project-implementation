using System.Net;
using NAUCountryA.Tables;
using System.Data;

namespace NAUCountryA.Models
{
    public class Practice : IEquatable<Practice>
    {
        public Practice(int practiceCode, string practiceName, string practiceAbbreviation, int commodityCode, DateTime releasedDate, string recordTypeCode)
        {
            PracticeCode = practiceCode;
            PracticeName = practiceName;
            PracticeAbbreviation = practiceAbbreviation;
            IReadOnlyDictionary<int,Commodity> commodityEntries = new CommodityTable();
            Commodity = commodityEntries[commodityCode];
            ReleasedDate = releasedDate;
            IReadOnlyDictionary<string,RecordType> recordTypeEntries = new RecordTypeTable();
            RecordType = recordTypeEntries[recordTypeCode];
        }

        public Practice(DataRow row)
        :this((int)row["PRACTICE_CODE"], (string)row["PRACTICE_NAME"], (string)row["PRACTICE_ABBREVIATION"], (int)row["COMMODITY_CODE"], (DateTime)row["RELEASED_DATE"], (string)row["RECORD_TYPE_CODE"])
        {
        }

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

        public KeyValuePair<int,Practice> Pair
        {
            get
            {
                return new KeyValuePair<int,Practice>(PracticeCode, this);
            }
        }

        public bool Equals(Practice other)
        {
            return PracticeCode == other.PracticeCode && PracticeName == other.PracticeName &&
            PracticeAbbreviation == other.PracticeAbbreviation && Commodity == other.Commodity &&
            Service.DateTimeEquals(ReleasedDate, other.ReleasedDate) && RecordType == other.RecordType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Practice))
            {
                return false;
            }
            return Equals((Practice)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator== (Practice a, Practice b)
        {
            return a.Equals(b);
        }

        public static bool operator!= (Practice a, Practice b)
        {
            return !a.Equals(b);
        }
    }
}