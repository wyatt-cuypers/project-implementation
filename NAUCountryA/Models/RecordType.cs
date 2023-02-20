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

        public KeyValuePair<string,RecordType> KeyValue
        {
            get
            {
                return new KeyValuePair<string, RecordType>(RecordTypeCode, this);
            }
        }
    }
}