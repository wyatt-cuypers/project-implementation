

namespace NAUCountryA.Models
{
    public class Offer : IEquatable<Offer>
    {
        // Assigned to Miranda Ryan
        public Offer(int offerID, int practiceCode, int countyCode, int typeCode, int irrigationPracticeCode, int year)
        {
            OfferID = offerID;
            Practice = Service.PracticeEntries[practiceCode];
            County = Service.CountyEntries[countyCode];
            Type = Service.TypeEntries[typeCode];
            IrrigationPracticeCode = irrigationPracticeCode;
            Year = year;
        }

        public Offer(string line)
        {
            string[] values = line.Split(',');
            int practiceCode = (int)Service.ExpressValue(values[4]);
            int countyCode = (int)Service.ExpressValue(values[2]);
            int typeCode = (int)Service.ExpressValue(values[3]);
            Valid = ValidOffer(practiceCode, countyCode, typeCode);
            if (Valid)
            {
                OfferID = (int)Service.ExpressValue(values[0]);
                Practice = Service.PracticeEntries[(int)Service.ExpressValue(values[4])];
                County = Service.CountyEntries[(int)Service.ExpressValue(values[2])];
                Type = Service.TypeEntries[(int)Service.ExpressValue(values[3])];
                IrrigationPracticeCode = (int)Service.ExpressValue(values[5]);
                Year = Convert.ToInt32(values[6]);
            }
        }

        public int OfferID
        {
            get;
            private set;
        }

        public Practice Practice
        {
            get;
            private set;
        }

        public County County
        {
            get;
            private set;
        }

        public NAUType Type
        {
            get;
            private set;
        }

        public int IrrigationPracticeCode
        {
            get;
            private set;
        }

        public int Year
        {
            get;
            private set;
        }

        public bool Valid
        {
            get;
            private set;
        }
        public KeyValuePair<int, Offer> Pair
        {
            get
            {
                return new KeyValuePair<int, Offer>(OfferID, this);
            }
        }

        public bool Equals(Offer other)
        {
            return OfferID == other.OfferID && Practice == other.Practice &&
            County == other.County && Type == other.Type && IrrigationPracticeCode == other.IrrigationPracticeCode
            && Year == other.Year;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Offer))
            {
                return false;
            }
            return Equals((Offer)obj);
        }

        public string FormatOfferID()
        {
            if (OfferID < 10)
            {
                return $"\"0000000{OfferID}\"";
            }
            else if (OfferID < 100)
            {
                return $"\"000000{OfferID}\"";
            }
            else if (OfferID < 1000)
            {
                return $"\"00000{OfferID}\"";
            }
            else if (OfferID < 10000)
            {
                return $"\"0000{OfferID}\"";
            }
            else if (OfferID < 100000)
            {
                return $"\"000{OfferID}\"";
            }
            else if (OfferID < 1000000)
            {
                return $"\"00{OfferID}\"";
            }
            else if (OfferID < 10000000)
            {
                return $"\"0{OfferID}\"";
            }
            return $"\"{OfferID}\"";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FormatOfferID()},{Practice.FormatPracticeCode()}," +
                $"{County.FormatCountyCode()},{Type.FormatTypeCode()},\"{IrrigationPracticeCode}\"," +
                $"{Year}";
        }

        public static bool operator ==(Offer a, Offer b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Offer a, Offer b)
        {
            return !a.Equals(b);
        }

        private bool ValidOffer(int practiceCode, int countyCode, int typeCode)
        {
            if (!Service.PracticeEntries.ContainsKey(practiceCode))
            {
                Console.WriteLine($"Practice Code #{practiceCode} doesn't exist.");
                return false;
            }
            else if (!Service.CountyEntries.ContainsKey(countyCode))
            {
                Console.WriteLine($"County Code #{countyCode} doesn't exist.");
                return false;
            }
            else if (!Service.TypeEntries.ContainsKey(typeCode))
            {
                Console.WriteLine($"Type Code #{typeCode} doesn't exist.");
                return false;
            }
            return true;
        }
    }
}