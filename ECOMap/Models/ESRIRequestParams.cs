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
                sybmol.Add("color", SymbolColor);
                JObject outline = new JObject();
                outline.Add("type", "esriSLS");
                outline.Add("color", new byte[]{0,0,0,255});
                outline.Add("width", 0.75);
                outline.Add("style", "esriSLSSolid");
                sybmol.Add("outline", outline);
                sybmol.Add("style", "esriSLSSolid");
                obj.Add("symbol", sybmol);
                obj.Add("value", $"{county.State.StateCode}{county.CountyCode}");
                return obj;
            }
        }

        private byte[] SymbolColor
        {
            get
            {
                string hexValue = ColorHexRepresentation.TrimStart('#');
                byte[] bytes = new byte[hexValue.Length / 2];
                for (int i = 0; i < hexValue.Length; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hexValue.Substring(i, 2), 16);
                }
                return bytes;
            }
        }

        private string ColorHexRepresentation
        {
            get
            {
                if (percentChange < -.04)
                {
                    return "#DE2D26";
                }
                else if (percentChange <= -.02)
                {
                    return "#FEE0D2";
                }
                else if (percentChange <= 0)
                {
                    return "#FFFFFF";
                }
                else if (percentChange <= 0.02)
                {
                    return "#E5F5E0";
                }
                else if (percentChange <= 0.04)
                {
                    return "#A1D99B";
                }
                return "#31A354";
            }
        }
    }
}