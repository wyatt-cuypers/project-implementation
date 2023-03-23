

namespace NAUCountryA.Models
{
    public class NAUType : IEquatable<NAUType>
    {
        // Assigned to Katelyn Runsvold
        public NAUType(int typeCode, string typeName, string typeAbbreviation,
            int commodityCode, DateTime releasedDate, string recordTypeCode)
        {
            TypeCode = typeCode;
            TypeName = typeName;
            TypeAbbreviation = typeAbbreviation;
            Commodity = Service.CommodityEntries[commodityCode];
            ReleasedDate = releasedDate;
            RecordType = Service.RecordTypeEntries[recordTypeCode];
        }

        public NAUType(string line)
        {
            string[] values = line.Split(',');
            TypeCode = (int)Service.ExpressValue(values[4]);
            TypeName = (string)Service.ExpressValue(values[5]);
            TypeAbbreviation = (string)Service.ExpressValue(values[6]);
            Commodity = Service.CommodityEntries[(int)Service.ExpressValue(values[3])];
            ReleasedDate = (DateTime)Service.ExpressValue(values[8]);
            RecordType = Service.RecordTypeEntries[(string)Service.ExpressValue(values[0])];
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

        public KeyValuePair<int, NAUType> Pair
        {
            get
            {
                return new KeyValuePair<int, NAUType>(TypeCode, this);
            }
        }

        public bool Equals(NAUType other)
        {
            return TypeCode == other.TypeCode &&
                TypeName == other.TypeName &&
                TypeAbbreviation == other.TypeAbbreviation &&
                Commodity == other.Commodity &&
                Service.DateTimeEquals(ReleasedDate, other.ReleasedDate) &&
                RecordType == other.RecordType;
        }

        public string FormatTypeCode()
        {
            if (TypeCode < 10)
            {
                return $"\"00{TypeCode}\"";
            }
            else if (TypeCode < 100)
            {
                return $"\"0{TypeCode}\"";
            }
            return $"\"{TypeCode}\"";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NAUType))
            {
                return false;
            }
            return Equals((NAUType)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatTypeCode()},\"{TypeName}\",\"{TypeAbbreviation}\",{Commodity.FormatCommodityCode()}" +
                $"\"{Service.ToString(ReleasedDate)}\",\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator ==(NAUType a, NAUType b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(NAUType a, NAUType b)
        {
            return !a.Equals(b);
        }
    }
}