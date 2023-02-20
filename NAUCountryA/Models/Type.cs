using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class Type
    {
        public Type(int typeCode, string typeName, string typeAbbreviation, 
            Commodity commodity, DateTime releasedDate, RecordType recordType)
        {
            TypeCode = typeCode;
            TypeName = typeName;
            TypeAbbreviation = typeAbbreviation;
            this.Commodity = commodity;
            ReleasedDate = releasedDate;
            this.RecordType = recordType;
        }

        public int TypeCode
        {
            get;
            private set;
        }

        public string TypeName
        {
            get;
            private set;
        }

        public string TypeAbbreviation
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
    }
}