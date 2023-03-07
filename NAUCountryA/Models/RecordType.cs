

namespace NAUCountryA.Models
{
    public class RecordType : IEquatable<RecordType>
    {
        public RecordType(string recordTypeCode, int recordCategoryCode, int reinsuranceYear)
        {
            RecordTypeCode = recordTypeCode;
            RecordCategoryCode = recordCategoryCode;
            ReinsuranceYear = reinsuranceYear;
        }

        public RecordType (string line)
        {
            string[] values = line.Split(',');
            RecordTypeCode = (string)Service.ExpressValue(values[0]);
            RecordCategoryCode = (int)Service.ExpressValue(values[1]);
            ReinsuranceYear = (int)Service.ExpressValue(values[2]);
        }

        public string RecordTypeCode
        {
            get;
            private set;
        }

        public int RecordCategoryCode
        {
            get;
            private set;
        }

        public int ReinsuranceYear
        {
            get;
            private set;
        }

        public KeyValuePair<string,RecordType> Pair
        {
            get
            {
                return new KeyValuePair<string, RecordType>(RecordTypeCode, this);
            }
        }

        public bool Equals(RecordType other)
        {
            return RecordTypeCode == other.RecordTypeCode && RecordCategoryCode == other.RecordCategoryCode
                && ReinsuranceYear == other.ReinsuranceYear;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RecordType))
            {
                return false;
            }
            return Equals((RecordType)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"\"{RecordTypeCode}\",{FormatRecordCategoryCode()},\"{ReinsuranceYear}\"";
        }

        public static bool operator== (RecordType a, RecordType b)
        {
            return a.Equals(b);
        }

        public static bool operator!= (RecordType a, RecordType b)
        {
            return !a.Equals(b);
        }

        private string FormatRecordCategoryCode()
        {
            if (RecordCategoryCode < 10)
            {
                return $"\"0{RecordCategoryCode}\"";
            }
            return $"\"{RecordCategoryCode}\"";
        }
    }
}