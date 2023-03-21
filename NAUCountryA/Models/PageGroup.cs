namespace NAUCountryA.Models
{
    public class PageGroup
    {
        public PageGroup(Practice practice, NAUType type)
        {
            Practice = practice;
            Type = type;
            Prices = new List<Price>();
        }

        public Practice Practice
        {
            get;
            set;
        }
        public NAUType Type
        {
            get;
            set;
        }

        public List<Price> Prices
        {
            get;
            set;
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

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}