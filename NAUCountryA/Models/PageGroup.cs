namespace NAUCountryA.Models
{
    public class PageGroup
    {
        public PageGroup(Practice practice, NAUType type)
        {
            Practice = practice;
            Type = type;
            // Consider (Since duplicates aren't allowed and order doesn't matter, choose a set to result less lines of code)
            Prices = new HashSet<Price>();
            // Rather Than
            // Prices = new List<Price>();
        }
        public void addPrice(Price price)
        {
            Prices.Add(price);
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
    }
}