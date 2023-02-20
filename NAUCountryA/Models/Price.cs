using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class Price
    {
        public Price(Offer offer, double expectedIndexValue = 0.0)
        {
            Offer = offer;
            ExpectedIndexValue = expectedIndexValue;
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
    }
}