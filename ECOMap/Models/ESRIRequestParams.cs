using Newtonsoft.Json.Linq;
namespace ECOMap.Models
{
    public class ESRIRequestParams
    {
        private readonly County county;
        private readonly double percentChange;
        public ESRIRequestParams (County county, double percentChange)
        {
            this.county = county;
            this.percentChange = percentChange;
        }

        public JObject UniqueValueInfo
        {
            get
            {
                JObject obj = new JObject();
                JObject sybmol = new JObject();
                sybmol.Add("type", "esriSFS");
                sybmol.Add("color", ColorRepresentation);
                JObject outline = new JObject();
                outline.Add("type", "esriSLS");
                outline.Add("color", "[0,0,0,255]");
                outline.Add("width", 0.75);
                outline.Add("style", "esriSLSSolid");
                sybmol.Add("outline", outline);
                sybmol.Add("style", "esriSLSSolid");
                obj.Add("symbol", sybmol);
                obj.Add("value", $"{EcoGeneralService.RemoveQuotationsFromCurrentFormat(county.State.FormatStateCode())}{EcoGeneralService.RemoveQuotationsFromCurrentFormat(county.FormatCountyCode())}");
                return obj;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not ESRIRequestParams)
            {
                return false;
            }
            ESRIRequestParams other = (ESRIRequestParams)obj;
            return county == other.county;
        }

        public override int GetHashCode()
        {
            return county.CountyCode.GetHashCode();
        }

        private string ColorRepresentation
        {
            get
            {
                if (percentChange < -.04)
                {
                    return "[222, 45, 38, 255]";
                }
                else if (percentChange <= -.02)
                {
                    return "[255, 165, 120, 255]";
                }
                else if (percentChange < 0)
                {
                    return "[254, 224, 210, 255]";
                }
                else if (percentChange == 0)
                {
                    return "[255, 255, 255, 255]";
                }
                else if (percentChange <= 0.02)
                {
                    return "[161, 217, 155, 255]";
                }
                else if (percentChange <= 0.04)
                {
                    return "[40, 144, 58, 255]";
                }
                return "[5, 79, 41, 255]";
            }
        }
    }
}