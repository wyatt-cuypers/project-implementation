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
                sybmol.Add("color", ColorHexRepresentation);
                JObject outline = new JObject();
                outline.Add("type", "esriSLS");
                JArray black = new JArray();
                black.Add(0);
                black.Add(0);
                black.Add(0);
                black.Add(255);
                outline.Add("color", black);
                outline.Add("width", 0.75);
                outline.Add("style", "esriSLSSolid");
                sybmol.Add("outline", outline);
                sybmol.Add("style", "esriSFSSolid");
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

        private JArray ColorHexRepresentation
        {
            get
            {
                Console.WriteLine(percentChange);
                JArray darkRed = new JArray();
                JArray medRed = new JArray();
                JArray lightRed = new JArray();
                JArray white = new JArray();
                JArray lightGreen = new JArray();
                JArray medGreen = new JArray();
                JArray darkGreen = new JArray();
                if (percentChange < -.04)
                {
                    darkRed.Add(222);
                    darkRed.Add(45);
                    darkRed.Add(38);
                    darkRed.Add(255);

                    return darkRed;
                }
                else if (percentChange <= -.02)
                {
                    medRed.Add(255);
                    medRed.Add(165);
                    medRed.Add(120);
                    medRed.Add(255);
                    return medRed;
                }
                else if (percentChange < 0)
                {
                    lightRed.Add(254);
                    lightRed.Add(224);
                    lightRed.Add(210);
                    lightRed.Add(255);
                    return lightRed;
                }
                else if (percentChange == 0)
                {
                    white.Add(255);
                    white.Add(255);
                    white.Add(255);
                    white.Add(255);
                    return white;
                }
                else if (percentChange <= 0.02)
                {
                    lightGreen.Add(161);
                    lightGreen.Add(217);
                    lightGreen.Add(155);
                    lightGreen.Add(255);
                    return lightGreen;
                }
                else if (percentChange <= 0.04)
                {
                    medGreen.Add(40);
                    medGreen.Add(144);
                    medGreen.Add(58);
                    medGreen.Add(255);
                    return medGreen;
                }
                else if (percentChange > 0.04)
                {
                    darkGreen.Add(5);
                    darkGreen.Add(79);
                    darkGreen.Add(41);
                    darkGreen.Add(255);
                    return darkGreen;
                }
                else
                {
                    white.Add(255);
                    white.Add(255);
                    white.Add(255);
                    white.Add(255);
                    return white;
                }
            }
        }
    }
}