using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class Type : IEquatable<Type>
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

        public Type(DataRow row)
        {
            TypeCode = (int)row["TYPE_CODE"];
            TypeName = (string)row["TYPE_NAME"];
            TypeAbbreviation = (string)row["TYPE_ABBR"];
            Commodity = (Commodity)row["COMMODITY_CODE"];
            ReleasedDate = (DateTime)row["RELEASED_DATE"];
            RecordType = (RecordType)row["RECORD_TYPE_CODE"];
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

        public bool Equals(Type other)
        {
            return TypeCode == other.TypeCode &&
                TypeName == other.TypeName &&
                TypeAbbreviation == other.TypeAbbreviation &&
                Commodity == other.Commodity &&
                Service.DateTimeEquals(ReleasedDate, other.ReleasedDate) &&
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

        public static bool operator ==(Type a, Type b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Type a, Type b)
        {
            return !a.Equals(b);
        }
    }
}