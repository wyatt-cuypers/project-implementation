using NAUCountryA.Tables;
using System.Data;

namespace NAUCountryA.Models
{
    public class County : IEquatable<Commodity>
    {
        public County(int countyCode, int stateCode, string countyName, string recordTypeCode)
        {
            CountyCode = countyCode;
            StateCode = stateCode;
            CountyName = countyName;
            IReadOnlyDictionary<string,RecordType> recordTypeEntries = new RecordTypeTable();
            RecordType = recordTypeEntries[recordTypeCode];
        }

        public Commodity(DataRow row)
        :this((int)row["COUNTY_CODE"], (int)row["STATE_CODE"], (string)row["COUNTY_NAME"], (string)row["RECORD_TYPE_CODE"])
        {
        }

        public int CountyCode
        {
            get;
            private set;
        }

        public int StateCode
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

        public bool Equals(County other)
        {
            return CountyCode == other.CountyCode && StateCode == other.StateCode &&
            CountyName == other.CountyName && RecordType == other.RecordType;
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

        public static bool operator== (County a, County b)
        {
            return a.Equals(b);
        }

        public static bool operator!= (County a, County b)
        {
            return !a.Equals(b);
        }
    }
}