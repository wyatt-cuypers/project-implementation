namespace NAUCountry.ECOMap.Models
{
    public class ESRIRequestParams
    {
        private readonly County county;
        private readonly State state;
        private readonly double percentChange;
        public ESRIRequestParams (County county, State state, double percentChange)
        {
            this.county = county;
            this.state = state;
            this.percentChange = percentChange;
        }

        public byte[] SymbolColor
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

        public string DefinitionExpression
        {
            get
            {
                return $"CNT_STATE_FIPS = '{state.StateCode}'";
            }
        }

        public string Value
        {
            get
            {
                return $"{state.StateCode}{county.CountyCode}";
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