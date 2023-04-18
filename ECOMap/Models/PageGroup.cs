namespace ECOMap.Models
{
    public class PageGroup
    {
        public PageGroup(Practice practice, NAUType type)
        {
            Practice = practice;
            Type = type;
            Prices = new HashSet<Price>();
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