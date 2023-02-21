using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class RecordType : IEquatable<RecordType>
    {
        public RecordType(IReadOnlyDictionary<string,RecordType> recordTypeEntries, string recordTypeCode)
        {
            RecordTypeCode = recordTypeEntries[recordTypeCode].RecordTypeCode;
            RecordCategoryCode = recordTypeEntries[recordTypeCode].RecordCategoryCode;
            ReinsuranceYear = recordTypeEntries[recordTypeCode].ReinsuranceYear;
        }
        public RecordType(string recordTypeCode, int recordCategoryCode, int reinsuranceYear)
        {
            RecordTypeCode = recordTypeCode;
            RecordCategoryCode = recordCategoryCode;
            ReinsuranceYear = reinsuranceYear;
        }

        public RecordType (DataRow row)
        {
            RecordTypeCode = (string)row["RECORD_TYPE_CODE"];
            RecordCategoryCode = (int)row["RECORD_CATEGORY_CODE"];
            ReinsuranceYear = (int)row["REINSURANCE_YEAR"];
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
            return base.ToString();
        }

        public static bool operator== (RecordType a, RecordType b)
        {
            return a.Equals(b);
        }

        public static bool operator!= (RecordType a, RecordType b)
        {
            return !a.Equals(b);
        }
    }
}