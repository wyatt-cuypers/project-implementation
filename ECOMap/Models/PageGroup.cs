namespace ECOMap.Models
{
    public class PageGroup
    {
        public PageGroup(Practice practice, NAUType type)
        {
            Practice = practice;
            Type = type;
            Prices = new SortedSet<Price>(new ByCountyName());
            PreviousPrices = new HashSet<Price>();
        }
        public Practice Practice
        {
            get;
            private set;
        }
        public NAUType Type
        {
            get;
            private set;
        }

        public ICollection<Price> Prices
        {
            get;
            private set;
        }
        public ICollection<Price> PreviousPrices
        {
            get;
            private set;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PageGroup))
            {
                return false;
            }
            else
            {
                PageGroup pt = (PageGroup)obj;
                return this.Equals(pt);
            }
        }
        public bool Equals(PageGroup p)
        {
            return this.Practice.Equals(p.Practice) && this.Type.Equals(p.Type);
        }

        public override string ToString()
        {
            return $"{Practice.ToString()}\n{Type.ToString()}";
        }

        public class ByCountyName : IComparer<Price>
        {
            public int Compare(Price p1, Price p2)
            {
                return string.Compare(p1.Offer.County.CountyName, p2.Offer.County.CountyName, StringComparison.CurrentCulture);
            }
        }
    }
}