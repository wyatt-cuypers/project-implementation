using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class RecordType
    {
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
    }
}