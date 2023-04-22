using Newtonsoft.Json.Linq;
namespace ECOMap.Models
{
    public class ESRIRequestParams
    {
        private readonly County county;
        private readonly double percentChange;
        public ESRIRequestParams(County county, double percentChange)
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
                outline.Add("color", "#000000");
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
                    return "#DE2D26";
                }
                else if (percentChange <= -.02)
                {
                    return "#FFA578";
                }
                else if (percentChange < 0)
                {
                    return "#FEE0D2";
                }
                else if (percentChange == 0)
                {
                    return "#FFFFFF";
                }
                else if (percentChange <= 0.02)
                {
                    return "#A1D99B";
                }
                else if (percentChange <= 0.04)
                {
                    return "#28903A";
                }
                return "#054F29";
            }
        }
    }
}