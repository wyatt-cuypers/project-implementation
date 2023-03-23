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
                return pt.Practice == this.Practice && pt.Type == this.Type;
            }
        }

        public override string ToString()
        {
            return $"{Practice.ToString()}\n{Type.ToString()}";
        }
    }
}