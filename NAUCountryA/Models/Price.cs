

namespace NAUCountryA.Models
{
    public class Price : IEquatable<Price>
    {
        // Assigned to Katelyn Runsvold
        public Price(int offerID, double expectedIndexValue)
        {
            Offer = Service.OfferEntries[offerID];
            ExpectedIndexValue = expectedIndexValue;
        }

        public Price(string line)
        {
            string[] values = line.Split(',');
            int offerId = (int)Service.ExpressValue(values[0]);
            Valid = Service.OfferEntries.ContainsKey(offerId);
            if (Valid)
            {
                Offer = Service.OfferEntries[offerId];
                ExpectedIndexValue = (double)Service.ExpressValue(values[1]);
            }
            else
            {
                Console.WriteLine($"Offer ID {offerId} doesn't exist.");
            }
        }

        public Offer Offer
        {
            get;
            private set;
        }

        public double ExpectedIndexValue
        {
            get;
            private set;
        }

        public KeyValuePair<Offer, Price> Pair
        {
            get
            {
                return new KeyValuePair<Offer, Price>(Offer, this);
            }
        }

        public bool Valid
        {
            get;
            private set;
        }

        public bool Equals(Price other)
        {
            return Offer == other.Offer && ExpectedIndexValue == other.ExpectedIndexValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Price))
            {
                return false;
            }
            return Equals((Price)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Offer.FormatOfferID()},\"{Service.ToString(ExpectedIndexValue)}\"";
        }

        public static bool operator ==(Price a, Price b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Price a, Price b)
        {
            return !a.Equals(b);
        }
    }
}