

namespace NAUCountryA.Models
{
    public class Practice : IEquatable<Practice>
    {
        // Assigned to Wyatt Cuypers
        public Practice(int practiceCode, string practiceName, string practiceAbbreviation, int commodityCode, DateTime releasedDate, string recordTypeCode)
        {
            PracticeCode = practiceCode;
            PracticeName = practiceName;
            PracticeAbbreviation = practiceAbbreviation;
            Commodity = Service.CommodityEntries[commodityCode];
            ReleasedDate = releasedDate;
            RecordType = Service.RecordTypeEntries[recordTypeCode];
        }

        public Practice(string line)
        {
            string[] values = line.Split(',');
            string recordTypeCode = (string)Service.ExpressValue(values[0]);
            int commodityCode = (int)Service.ExpressValue(values[3]);
            Valid = ValidPractice(recordTypeCode, commodityCode);
            if (Valid)
            {
                PracticeCode = (int)Service.ExpressValue(values[4]);
                PracticeName = (string)Service.ExpressValue(values[5]);
                PracticeAbbreviation = (string)Service.ExpressValue(values[6]);
                Commodity = Service.CommodityEntries[commodityCode];
                ReleasedDate = (DateTime)Service.ExpressValue(values[8]);
                RecordType = Service.RecordTypeEntries[recordTypeCode];
            }
            
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

        public KeyValuePair<int, Practice> Pair
        {
            get
            {
                return new KeyValuePair<int, Practice>(PracticeCode, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
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

        public string FormatPracticeCode()
        {
            if (PracticeCode < 10)
            {
                return $"\"00{PracticeCode}\"";
            }
            else if (PracticeCode < 100)
            {
                return $"\"0{PracticeCode}\"";
            }
            return $"\"{PracticeCode}\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatPracticeCode()},\"{PracticeName}\",\"{PracticeAbbreviation}\"," +
                $"{Commodity.FormatCommodityCode()},\"{Service.ToString(ReleasedDate)}\",\"{RecordType.RecordTypeCode}\"";
        }

        public static bool operator ==(Practice a, Practice b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Practice a, Practice b)
        {
            return !a.Equals(b);
        }

        private bool ValidPractice(string recordTypeCode, int commodityCode)
        {
            if (!Service.RecordTypeEntries.ContainsKey(recordTypeCode))
            {
                Console.WriteLine($"Record Type Code {recordTypeCode} doesn't exist.");
                return false;
            }
            else if (!Service.CommodityEntries.ContainsKey(commodityCode))
            {
                Console.WriteLine($"Commodity Code {commodityCode} doesn't exist.");
                return false;
            }
            return true;
        }
    }
}